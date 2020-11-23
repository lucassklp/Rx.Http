using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rx.Http;
using Xunit;

namespace Tests
{
    public class NavigatorTests
    {
        private RxNavigator navigator;
        public NavigatorTests()
        {
            navigator = new RxNavigator(new ConsumerContext<RxNavigator>(new HttpClient()));
        }

        [Fact]
        public async Task TestCookieNavigation()
        {
            var values = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "Key", "Value" }
            };
            await navigator.Get("https://postman-echo.com/cookies/set", options =>
            {
                options.AddQueryString(values);
            });

            var response = await navigator.Get<JObject>("https://postman-echo.com/cookies");
            var cookies = response.GetValue("cookies").ToObject<Dictionary<string, string>>();
            Assert.Equal(cookies, values);
        }

        [Fact]
        public async Task TestCleanCookie()
        {
            var values = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "Key", "Value" }
            };
            await navigator.Get("https://postman-echo.com/cookies/set", options =>
            {
                options.AddQueryString(values);
            });
            var uri = new Uri("https://postman-echo.com");
            navigator.ClearCookies(uri);

            var cookies = navigator.GetCookies(uri);
            Assert.Empty(cookies);
        }

        [Fact]
        public async Task TestGetCookie()
        {
            var values = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "Key", "Value" }
            };
            await navigator.Get("https://postman-echo.com/cookies/set", options =>
            {
                options.AddQueryString(values);
            });

            var uri = new Uri("https://postman-echo.com");

            var cookies = navigator.GetCookies(uri);
            Assert.NotEmpty(cookies);
        }
    }
}
