using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Rx.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class NavigatorTests
    {
        private RxNavigator navigator;
        public NavigatorTests()
        {
            navigator = new RxNavigator(new HttpClient());
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
