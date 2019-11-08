using Models;
using Models.Postman;
using Rx.Http;
using Rx.Http.Exceptions;
using Rx.Http.MediaTypes;
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
        public async void TestPostWithJson()
        {
            var postWithId = await http.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts", new Post()
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
                { "Characters", "*&¨%6dbajs&@#chv73*(#Y" }
            };

            var headers = await http.Get<PostmanEchoResponse>(@"https://postman-echo.com/get", opts =>
            {
                foreach (var item in queryStrings)
                {
                    opts.QueryStrings.Add(item.Key, item.Value);
                }
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
                foreach (var item in headers)
                {
                    opts.AddHeader(item.Key, item.Value);
                }
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
                opts.RequestMediaType = MediaTypesMap.Get(MediaType.Application.Json);
            });

            Assert.True(response.Headers["content-type"] == MediaType.Application.Json);
        }
    }
}