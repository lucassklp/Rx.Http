<p align="center">
  <img src="Resources/rx.http.mini.png">
</p>
<p align="center">
<img alt="nuget" src="https://img.shields.io/nuget/dt/Rx.Http.svg">
<a href="https://www.codacy.com/manual/lucassklp/Rx.Http?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=lucassklp/Rx.Http&amp;utm_campaign=Badge_Grade">
    <img src="https://api.codacy.com/project/badge/Grade/90ffddf0fe1c4bb89e8e7049784ea190"/>
</a>
<img alt="npm version" src="https://img.shields.io/nuget/v/Rx.Http.svg">


<!-- snyk does not support .NET Core yet -->
<!-- https://github.com/snyk/snyk/issues/489 -->
<!-- <a href="https://snyk.io//test/github/lucassklp/Rx.Http?targetFile=Rx.Http/Rx.Http.csproj"><img src="https://snyk.io//test/github/lucassklp/Rx.Http/badge.svg?targetFile=Rx.Http/Rx.Http.csproj" alt="Known Vulnerabilities" data-canonical-src="https://snyk.io//test/github/lucassklp/Rx.Http?targetFile=Rx.Http/Rx.Http.csproj" style="max-width:100%;"></a> -->

</p>

A lightweight library that is inpired in [Angular 2+ Http Client](https://angular.io/guide/http) built on top of [.NET Http Client](https://docs.microsoft.com/pt-br/dotnet/api/system.net.http.httpclient) that help programmers to make asynchronous http requests, 


# Installation

If you are using Package Manager:

```bash
Install-Package Rx.Http -Version 1.0.1
```

If you are using .NET CLI

```bash
dotnet add package Rx.Http --version 1.0.1
```


## Example of use

```csharp
using Rx.Http;
using System.Reactive.Linq;

public class Program 
{
    public static async void Main()
    {
        //Initialize the RxHttpClient
        var http = new RxHttpClient(new HttpClient());

        //Retrieve a list of To-Do item and print the title of each element asynchronously
        http.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(items => {
            items.ForEach(item => Console.WriteLine(item.title));
        });

        //Making the same request using await
        List<Todo> response = await http.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/");
    }
}
```

### Options

You can customize your request by using options. It make possible you set **query strings, headers** and **your custom serializer and deserializer for your request or response**.

#### Let's dive in options

```csharp
http.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/", options =>
{
    options.SetRequestMediaType(new JsonHttpMediaType())
        .SetResponseMediaType(new JsonHttpMediaType())
        .AddHeader(new {
            Authorization = "Bearer <token>"
            Accept = "application/json"
        })
        .AddQueryString(new {
            name = "John Doe",
            index = 1
        });
});
```

The media type represents a structure that is used to translate a mime type to a object (serializing or deserializing).

**RequestMediaType** is used to serialize your body content. **By default is used JsonHttpMediaType**

**ResponseMediaType** is used to deserialize your body content. **By default it use the serializer based on mime type from response.**

You can customize your own *Media Type* by implementing the interface **IHttpMediaType**.

## Consumers

A consumer is defined as a service that have common behavior for the requests. You can encapsulate the logic of all those requests in a easy way.
The main advantage of using consumers is to abstract the HTTP request and its implementation details, and only work with the results from it.

### Interceptors

The interceptors are a pre-processing unit that changes the request before it happens. It can be usefull to configure the pattern for all requests.

### Example of use

In this example, we need to provide the api key for all requests to [The Movie Database API](https://developers.themoviedb.org/3/)

The code above shows how to use Consumers and Interceptors.

```csharp
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerConfiguration<TheMovieDatabaseConsumer> configuration): base(configuration)
        {
            configuration.AddRequestInterceptors(new TheMovieDatabaseInterceptor());
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.AddQueryString("api_key", "key");
        }
    }
```

## RxHttpRequestException
This exception is threw when the server reply with a HTTP Status different of 2xx. There's two ways to handle this exception:

```csharp
    var url = @"https://jsonplaceholder.typicode.com/this_page_dont_exist_hehehe/";

    //With traditional try-catch block
    try
    {
        var todos = await http.Get<List<Todo>>(url);
    }
    catch(RxHttpRequestException ex)
    {
        HttpResponseMessage response = ex.Response;
        //...
    }

    //Or using reactive way
    http.Get<List<Todo>>(url).Subscribe(response => 
    {
        //...
    }, exception => 
    {
        HttpResponseMessage response = (exception as RxHttpRequestException)?.Response;
        //...
    })

```

## Working with Dependency Injection

Is strongly recommended to use [DI (Dependency Injection)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) with Rx.Http, because of [HttpClientFactory](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests), that improves HttpClient performance. For that, you must do the following:

```csharp
public void ConfigureServices(ServiceCollection services)
{
    services.AddHttpClient<RxHttpClient>();

    //You **must** configure your consumer http client here
    services.AddConsumer<TheMovieDatabaseConsumer>(http =>
    {
        http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
    });
}
```

## Logging

You can implement your own custom logging mechanism by implementing the interface RxHttpLogging. 
We provide a built-in logging mechanism called "RxHttpDefaultLogging".

Here is a example that show how to use RxHttpDefaultLogging mechanism. If you have a custom logging mechanism you must replace RxHttpDefaultLogging for your class implementation.
```csharp
private static void ConfigureServices(ServiceCollection services)
{
    services.AddRxHttpLogging<RxHttpDefaultLogging>();
}
```

### Roadmap

#### Version 1.x

 * [x] **Reactive GET, POST, PUT DELETE Http Methods**
 * [x] **Built-in JSON serializing and deserializing (using Newtonsoft.Json)**
 * [x] **Support for custom serializing and deserializing**
 * [x] **Logging**
 * [x] **Consumers and Interceptors implementation**
 * [x] **Error handling**
 * [x] **Native support for x-www-form-urlencoded and form-data**

#### Version 2.x

 * [ ] **Implement cancellation token funcionality**
 * [ ] **Built-in XML support**
 * [ ] **Global settings**
 * [ ] **Provide a alternative for built-in Json serializer: System.Text.Json.JsonSerializer of .NET Core 3**
 * [ ] **ASP.Net Core native integration**
