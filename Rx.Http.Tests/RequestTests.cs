using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using Rx.Http.Serializers;
using Rx.Http.Tests.Models;
using Rx.Http.Tests.Models.Postman;
using Xunit;

namespace Rx.Http.Tests
{
    public class RequestTests
    {
        RxHttpClient http;
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
            //Invalid URL
            await Assert.ThrowsAsync<HttpRequestException>(async () => await http.Get<List<Todo>>(@"https://jsonplaceholder.typicode.com/this_page_dont_exist_hehehe/"));
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
            var queryStrings = new Dictionary<string, string>();
            queryStrings.Add("Foo", "Bar");
            queryStrings.Add("User", "John Doe");
            queryStrings.Add("Characters", "*&¨%6dbajs&@#chv73*(#Y");

            var headers = await http.Get<GetResponse>(@"https://postman-echo.com/get", opts => 
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
            var headers = new Dictionary<string, string>();
            headers.Add("Foo", "Bar");
            headers.Add("User", "John Doe");
            
            var response = await http.Get<GetResponse>(@"https://postman-echo.com/get", opts =>
            {
                foreach (var item in headers)
                {
                    opts.AddHeader(item.Key, item.Value);
                }
            });

            Assert.True(response.Headers.ContainsKey("foo") && response.Headers.ContainsKey("user"));
        }
    }
}