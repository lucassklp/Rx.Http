using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Models;
using Models.Postman;
using Rx.Http;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes;
using Xunit;
using Rx.Http.Extensions;

namespace Tests
{
    public class PostmanEchoTests
    {

        private readonly RxHttpClient http;
        public PostmanEchoTests()
        {
            http = new RxHttpClient(new HttpClient(), null);
        }

        [Fact]
        public async Task TestGet()
        {
            var response = await http.Get("https://postman-echo.com/get");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestPost()
        {
            var response = await http.Post("https://postman-echo.com/post");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestPut()
        {
            var response = await http.Put("https://postman-echo.com/put");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestDelete()
        {
            var response = await http.Delete("https://postman-echo.com/delete");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestPatch()
        {
            var response = await http.Request("https://postman-echo.com/patch", HttpMethod.Patch);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestBasicAuth()
        {
            var response = await http.Get("https://postman-echo.com/basic-auth", options =>
            {
                options.UseBasicAuthorization("postman", "password");
            });

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestPostWithJsonBody()
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
        public async Task TestQueryStrings()
        {
            var queryStrings = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" }
            };

            var headers = await http.Get<EchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                opts.AddQueryString(queryStrings);
            });

            Assert.Equal(headers.Args, queryStrings);
        }

        [Fact]
        public async Task TestQueryStringsOnUrl()
        {
            var queryStrings = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
            };

            var headers = await http.Get<EchoResponse>(@"https://postman-echo.com/get?Foo=Bar&User=John%20Doe");
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

            var headers = await http.Get<EchoResponse>(@"https://postman-echo.com/get", opts =>
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

            var response = await http.Get<EchoResponse>(@"https://postman-echo.com/get", opts =>
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

            var response = await http.Get<EchoResponse>(@"https://postman-echo.com/get", opts =>
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
            var response = await http.Post<EchoResponse>(@"https://postman-echo.com/post", new
            {
                JsonProperty = "Http is nice",
                AnotherProperty = "But with Rx is awesome"
            });

            Assert.True(response.Headers["content-type"] == MediaType.Application.Json);
        }

        [Fact]
        public async Task TestGetWithJson()
        {
            var response = await http.Get<EchoResponse>(@"https://postman-echo.com/get", new
            {
                JsonProperty = "Http is nice",
                AnotherProperty = "But with Rx is awesome"
            });


            var isCorrect = response.Args["JsonProperty"] == "Http is nice"
                && response.Args["AnotherProperty"] == "But with Rx is awesome";

            Assert.True(isCorrect);
        }


        [Fact]
        public async Task TestRequestInterceptor()
        {
            var response = await http.Post<EchoResponse>(@"https://postman-echo.com/post", opts =>
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
