# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

Rx.Http is a lightweight .NET HTTP client library (inspired by Angular's HttpClient) built on top of `System.Net.Http.HttpClient` and Rx.NET (`System.Reactive`). Every request returns an `IObservable<T>`, which can also be `await`ed directly. It targets `netstandard2.0`, `net8.0`, and `net10.0`. Published to NuGet as `Rx.Http`.

## Commands

```bash
dotnet restore
dotnet build --configuration Release --no-restore

# Run the full test suite against a specific TFM (tests target net8.0 and net10.0)
dotnet test --framework net8.0
dotnet test --framework net10.0

# Run a single test
dotnet test --framework net8.0 --filter "FullyQualifiedName~RequestTests.TestGetRequest"

# Run everything in one test class
dotnet test --framework net8.0 --filter "ClassName=Rx.Http.Tests.NavigatorTests"
```

CI (`.github/workflows/build-and-test-develop.yml`, `build-test-publish.yml`) restores, builds, and runs `dotnet test` separately per TFM with coverage collection, then reports to Codacy. The `build-test-publish.yml` workflow also packs and pushes to GitHub Packages and NuGet.org on push to `main`.

**Tests hit live third-party HTTP endpoints** (jsonplaceholder.typicode.com, postman-echo.com, api.themoviedb.org) — there are no mocks/stubs for `HttpClient`. Expect tests to require network access and to be flaky/slow if those services are unavailable.

## Solution layout

The `.sln` has four projects:
- **Rx.Http** — the library itself.
- **Models** — POCOs and example `RxHttpClient` subclasses (`Consumers/`) used by tests and samples (e.g. `JsonPlaceHolderConsumer`, `PostmanConsumer`, `TheMovieDatabaseConsumer`).
- **Rx.Http.Tests** — xUnit test project (`net8.0`/`net10.0`), references `Models` and `Rx.Http`.
- **Samples** — a runnable console app demonstrating usage.

Note: there is also a top-level `Tests/` directory containing an older, near-duplicate `ConsumersTests.cs`. It is **not** referenced by `Rx.Http.sln` and is not part of the build — treat it as stale; the active tests live in `Rx.Http.Tests/`.

## Architecture

### Request pipeline

`RxHttpClient` (`Rx.Http/RxHttpClient.cs`) is the entry point (`RxHttpClient.Create()` or via DI). It exposes `Get`/`Post`/`Put`/`Patch`/`Delete` overloads, all of which funnel into one of the private `Request(...)` methods:

1. A `RxHttpRequest` is constructed from the URL, the client's `RequestInterceptors`/`ResponseInterceptors` lists, an optional body object, and an optional `Action<RxHttpRequestOptions>` configurator.
2. `SingleObservable.Create(...)` wraps a single async operation as a cold `IObservable<T>` that emits once and completes (or errors) — this is the mechanism that lets every call be both `Subscribe`d and `await`ed.
3. Inside that operation: request interceptors run (`RxRequestInterceptor.Intercept(RxHttpRequestOptions)`), the `HttpRequestMessage` is built (URL + query strings via `BuildUrl`, body via `RxHttpRequest.BuildContent()`, headers), the logger's `OnSend`/`OnReceive` hooks fire around the actual `HttpClient.SendAsync`, then response interceptors run (`RxResponseInterceptor.Intercept(HttpResponseMessage)`), and the result is wrapped as `RxHttpResponse`.
4. For typed calls (`Get<T>`, etc.), `RxHttpResponseMessageExtensions.Content<T>()` (in `Extensions/`) deserializes the body via `RxHttpRequest.ResponseMediaType` and throws `RxHttpRequestException` (wrapping the original `RxHttpResponse`) on non-2xx status or deserialization failure.

### Options / configuration

`RxHttpRequestOptions` is an abstract base defining the fluent configuration surface (`AddHeader`, `AddQueryString`, `AddRequestInteceptor`, `AddResponseInterceptor`, `SetRequestMediaType`, `SetResponseMediaType`). `RxHttpRequest` (`Rx.Http/RxHttpRequest.cs`) is the concrete implementation and also holds per-request state: `Headers`/`QueryStrings` (backed by `ListDictionary<TKey,TValue>`, a multi-value dictionary), the request body `Content`, and the resolved `RequestMediaType`/`ResponseMediaType` (defaulted from `RxHttp.Default`, see below).

### Global defaults

`RxHttp.Default` (`Rx.Http/RxHttp.cs`) is a static holder for library-wide defaults: `Serializable`, `RequestMediaType`, and `ResponseMediaType`, all defaulting to `NativeJsonSerializer` (`System.Text.Json`)-based implementations. Overriding these (e.g. swapping in `NewtonsoftJsonSerializer`) affects every subsequently created `RxHttpRequest` that doesn't explicitly set its own media types.

### Media types & serializers

`MediaTypes/Abstractions` defines `IHttpMediaTypeSerializer` (object → `HttpContent`) and `IHttpMediaTypeDeserializer` (`Stream` → `T`); `IHttpMediaType` combines both. `JsonHttpMediaType` is the built-in implementation, delegating actual (de)serialization to an `ITwoWaysSerializer` (`Serializers/` — `NewtonsoftJsonSerializer`, `NativeJsonSerializer` for `System.Text.Json`, `XmlSerializer`). Custom formats (XML, CSV, etc.) are added by implementing these interfaces and calling `SetRequestMediaType`/`SetResponseMediaType` in request options.

### Interceptors & Consumers

`RxRequestInterceptor`/`RxResponseInterceptor` (`Interceptors/`) are the pre/post-processing hooks, added per-client via `RequestInterceptors`/`ResponseInterceptors`. The intended pattern is to subclass `RxHttpClient` as a "Consumer" (see `Models/Consumers/*.cs`) that configures a base address and interceptors in its constructor and exposes typed methods — analogous to Spring's `FeignClient`. `RxNavigator` (`Rx.Http/RxNavigator.cs`) is a built-in example: it subclasses `RxHttpClient` and uses a request/response interceptor pair (`CookieInterceptor`/`SetCookieInterceptor`) to transparently track and replay cookies per-host, simulating a browser session.

### Logging

`RxHttpLogger` (`Logging/`) is the logging interface, invoked as `OnSend`/`OnReceive` around each request in `RxHttpClient.Request`. `HttpLoggerBase` provides shared formatting logic; `RxHttpDefaultLogger` uses `Microsoft.Extensions.Logging`, `RxHttpConsoleLogger` is a dependency-free fallback. Register via `IServiceCollection.AddRxHttpLogger<T>()` (`Extensions/RxHttpLoggingExtensions.cs`) or `RxHttpClient.UseLogger(...)`.

### Dependency injection

`Extensions/RxHttpClientFactoryExtensions.UseRxHttp()` registers `RxHttpClient` via `IHttpClientFactory` (`AddHttpClient<RxHttpClient>()`), which is the recommended way to consume this library in DI-based apps (see `Rx.Http.Tests/Injector.cs` for the pattern used across the test suite).

### Response extensions

`Extensions/RxHttpResponseMessageExtensions.cs` adds `Content<T>()` (deserialize + error handling, described above), `AsString()`, `ToFile(path)` (streams the response body to disk — used for downloads), and `AsHtmlDocument()` (parses the body via `HtmlAgilityPack`, intended to pair with `RxNavigator` for scraping-style flows).

## Multi-targeting gotchas

Several files branch on `#if NETSTANDARD2_0` (e.g. `RxHttpClient`'s `PatchMethod`, `RxHttpResponse.TrailingHeaders`) because APIs like `HttpMethod.Patch` and `HttpResponseMessage.TrailingHeaders` don't exist on `netstandard2.0`. When touching code paths that use newer BCL APIs, check whether an `netstandard2.0`-compatible fallback is needed.
