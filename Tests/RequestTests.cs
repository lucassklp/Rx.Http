using Models;
using Models.Postman;
using Rx.Http;
using Rx.Http.Exceptions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes;
using Rx.Http.Serializers;
using Rx.Http.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using System.IO;

namespace Tests
{
    public class RequestTests
    {
        private readonly RxHttpClient http;
        public RequestTests()
        {
            http = new RxHttpClient(new HttpClient(), null);
        }

        [Fact]
        public async Task TestGetAsStringContent()
        {
            var response = await http.Get("http://google.com");
            Assert.NotNull(response);
        }

        [Fact]
        public async Task TestGetAsJsonObject()
        {
            var todos = await http.Get<List<Todo>>(@"https://jsonplaceholder.typicode.com/todos/");

            Assert.NotNull(todos);
        }

        [Fact]
        public async Task TestGet404Error()
        {
            await Assert.ThrowsAsync<RxHttpRequestException>(async () =>
            {
                //Invalid URL
                var url = @"https://jsonplaceholder.typicode.com/this_page_dont_exist_hehehe/";
                await http.Get<List<Todo>>(url);
            });
        }

        [Fact]
        public async Task TestBodyWithJson()
        {
            var todo = new Todo
            {
                Id = 12,
                Title = "Testing with special characters: àáç~ã*+ü?<>!ºªª",
                IsCompleted = true,
                UserId = 20
            };
            var response = await http.Post<PostResponse<Todo>>("https://postman-echo.com/post", todo);

            Assert.True(response.Data.Equals(todo));
        }


        [Fact]
        public async Task TestPostWithJson()
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
        public async Task TestQueryStrings()
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
        public async Task TestQueryStringsWithObject()
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
        public async Task TestHeaders()
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
        public async Task TestHeadersWithObject()
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
        public async Task TestJsonContentTypeInHeader()
        {
            var response = await http.Post<PostmanEchoResponse>(@"https://postman-echo.com/post", new
            {
                JsonProperty = "Http is nice",
                AnotherProperty = "But with Rx is awesome"
            }, opts =>
            {
                opts.SetRequestMediaType(new JsonHttpMediaType(new NewtonsoftJsonSerializer()));
            });

            Assert.True(response.Headers["content-type"] == MediaType.Application.Json);
        }

        [Fact]
        public async Task TestDownloadFile()
        {
            var fileName = $"mysql-installer-web-community-8.0.22.0.msi";
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            await http.Get($@"https://dev.mysql.com/get/Downloads/MySQLInstaller/{fileName}")
                .ToFile(path);

            Assert.True(File.Exists(path));
        }

        [Fact]
        public async Task TestRequestInterceptor()
        {
            var response = await http.Post<PostmanEchoResponse>(@"https://postman-echo.com/post", opts =>
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