# Rx.Http

A lightweight library that is inpired in [Angular Http Client](https://angular.io/guide/http) and help programmers to make asynchronous http requests.

**Note: This project is under development.**

## Example of use

```csharp
using Rx.Http;
using System.Reactive.Linq;

//Retrieve a list of To-Do item and print the title of each element asynchronously
request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
    itens.ForEach(item => Console.WriteLine(item.title));
});

//Making the same request using await
List<Todo> response = await request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/");

```

### Options

You can customize your request by using options. It make possible you set **query strings, headers** and **your custom serializer and deserializer for your request or response**.

#### Let's dive in options

```csharp
httpClient.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/", options =>
{
    options.RequestMediaType = new JsonHttpMediaType();
    options.ResponseMediaType = new JsonHttpMediaType();
    options.AddHeader("Authorization", "Bearer <token>");
    options.QueryStrings.Add("name", "John Doe");
})
```

The media type represents a structure that is used to translate a mime type to a object (serializing or deserializing).

**RequestMediaType** is used to serialize your body content. **By default is JsonHttpMediaType**

**ResponseMediaType** is used to deserialize your body content. **By default it use the serializer based on mime type from response.**

You can customize your own *Media Type* by implementing the abstract class **HttpMediaType**.

## Consumers

A consumer is defined as a service that follow a pattern for all requests. You can encapsulate the logic of all those requests in a easy way.
The main advantage of using consumers is to make a abstraction of the HTTP request and its implementation details, and only work with the results from it.

### Interceptors

The interceptors are a pre-processing unit that changes the request before it happens. It can be usefull to configure the pattern for all requests.

### Example of use

In this example, we need to provide the api key for all requests to [The Movie Database API](https://developers.themoviedb.org/3/)

The code above shows how to use Consumers and Interceptors.

```csharp

    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(ILogger<RxHttpClient> logger): base(new RxHttpClient(new HttpClient(), logger))
        {

        }
        public override RxHttpRequestConventions Setup()
        {
            var conventions = new RxHttpRequestConventions();
            conventions.BaseUrl = @"https://api.themoviedb.org/3/";
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            return conventions;
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.QueryStrings.Add("api_key", "key");
        }
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
 * [ ] **Native support for x-www-form-urlencoded and form-data**

#### Version 2.x

 * [ ] **Implement cancellation token funcionality**
 * [ ] **Built-in XML support**
 * [ ] **Global settings**
 * [ ] **Provide a alternative for built-in Json serializer: System.Text.Json.JsonSerializer of .NET Core 3**
 * [ ] **ASP.Net Core native integration**
