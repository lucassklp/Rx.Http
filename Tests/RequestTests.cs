using Models;
using Models.Postman;
using Rx.Http;
using Rx.Http.Exceptions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes;
using Rx.Http.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using Xunit;

namespace Tests
{
    public class RequestTests
    {
        private readonly RxHttpClient http;
        public RequestTests()
        {
            http = new RxHttpClient(new HttpClient());
        }

        [Fact]
        public async void TestGetAsStringContent()
        {
            var response = await http.Get("http://google.com");
            Assert.NotNull(response);
        }

        [Fact]
        public async void TestGetAsJsonObject()
        {
            var todos = await http.Get<List<Todo>>(@"https://jsonplaceholder.typicode.com/todos/");

            Assert.NotNull(todos);
        }

        [Fact]
        public async void TestGet404Error()
        {
            await Assert.ThrowsAsync<RxHttpRequestException>(async () =>
            {
                //Invalid URL
                var url = @"https://jsonplaceholder.typicode.com/this_page_dont_exist_hehehe/";
                await http.Get<List<Todo>>(url);
            });
        }

        [Fact]
        public void TestBodyWithJson()
        {
            var todo = new Todo
            {
                Id = 12,
                Title = "Testing with special characters: àáç~ã*+ü?<>!ºªª",
                IsCompleted = true,
                UserId = 20
            };
            var response = http.Post<PostResponse<Todo>>("https://postman-echo.com/post", todo).Wait();

            Assert.True(response.Data.Equals(todo));
        }


        [Fact]
        public async void TestPostWithJson()
        {
            var postWithId = await http.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts/", new Post()
            {
                Title = "Foo",
                Body = "Bar",
                UserId = 3
            });

            Assert.NotNull(postWithId);
        }

        [Fact]
        public async void TestQueryStrings()
        {
            var queryStrings = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" }
            };

            var headers = await http.Get<PostmanEchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                opts.AddQueryString(queryStrings);
            });

            Assert.Equal(headers.Args, queryStrings);
        }

        [Fact]
        public async void TestQueryStringsWithObject()
        {
            var queryStrings = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" }
            };

            var queryStringsObj = new 
            {
                Foo = "Bar",
                User = "John Doe",
                Characters = "*&�%6dbajs&@#chv73*(#Y"
            };

            var headers = await http.Get<PostmanEchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                opts.AddQueryString(queryStringsObj);
            });

            Assert.Equal(headers.Args, queryStrings);
        }

        [Fact]
        public async void TestHeaders()
        {
            var headers = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" }
            };

            var response = await http.Get<PostmanEchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                opts.AddHeader(headers);
            });

            var valid = headers.All(i =>
            {
                //Postman echo brings lowercase key
                return response.Headers[i.Key.ToLower()] == i.Value;
            });
            Assert.True(valid);
        }

        [Fact]
        public async void TestHeadersWithObject()
        {
            var headers = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" }
            };

            var headersObj = new 
            {
                Foo = "Bar",
                User = "John Doe"
            };

            var response = await http.Get<PostmanEchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                opts.AddHeader(headersObj);
            });

            var valid = headers.All(i =>
            {
                //Postman echo brings lowercase key
                return response.Headers[i.Key.ToLower()] == i.Value;
            });
            Assert.True(valid);
        }

        [Fact]
        public async void TestJsonContentTypeInHeader()
        {
            var response = await http.Post<PostmanEchoResponse>(@"https://postman-echo.com/post", new
            {
                JsonProperty = "Http is nice",
                AnotherProperty = "But with Rx is awesome"
            }, opts =>
            {
                opts.SetRequestMediaType(MediaTypesMap.Get(MediaType.Application.Json));
            });

            Assert.True(response.Headers["content-type"] == MediaType.Application.Json);
        }


        [Fact]
        public async void TestRequestInterceptor()
        {
            var response = await http.Post<PostmanEchoResponse>(@"https://postman-echo.com/post", null, opts =>
            {
                opts.AddRequestInteceptor(new TestInterceptor());
            });

            Assert.True(response.Headers["accept"] == MediaType.Application.Json);
        }


        public class TestInterceptor : RxRequestInterceptor
        {
            public void Intercept(RxHttpRequestOptions request)
            {
                request.AddHeader("Accept", MediaType.Application.Json);
            }
        }
    }
}