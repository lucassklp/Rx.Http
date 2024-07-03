using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Models.Postman;
using Newtonsoft.Json.Linq;
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
        public async Task TestCookieNavigation2()
        {
            var values = new ListDictionary<string, string>();
            values.Append("IDENTITY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master/; HttpOnly");
            values.Append("IDENTITY_LEGACY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master/; HttpOnly");
            values.Append("SESSION", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master/");
            values.Append("SESSION_LEGACY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master/");
            values.Append("IDENTITY_LEGACY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master; HttpOnly");
            values.Append("IDENTITY_LEGACY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master; HttpOnly");
            values.Append("SESSION", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master");
            values.Append("SESSION_LEGACY", "; Version=1; Comment=Expiring cookie; Expires=Thu, 01-Jan-1970 00:00:10 GMT; Max-Age=0; Path=/auth/realms/master");
            values.Append("IDENTITY", "jwt.token.format; Version=1; Path=/auth/realms/master/; SameSite=None; Secure; HttpOnly");
            values.Append("IDENTITY_LEGACY", "jwt.token.format; Version=1; Path=/auth/realms/master/; HttpOnly");
            values.Append("SESSION", "master/869a91bb-370c-4c3a-82f4-5557e6131e88/fa2399ba-34a3-4cd2-afd3-ab06788181b1; Version=1; Expires=Tue, 31-Jan-2023 13:28:21 GMT; Max-Age=36000; Path=/auth/realms/master/; SameSite=None; Secure");
            values.Append("SESSION_LEGACY", "master/869a91bb-370c-4c3a-82f4-5557e6131e88/fa2399ba-34a3-4cd2-afd3-ab06788181b1; Version=1; Expires=Tue, 31-Jan-2023 13:28:21 GMT; Max-Age=36000; Path=/auth/realms/master/");

            await navigator.Get("https://postman-echo.com/cookies/set", options =>
            {
                options.AddQueryString(values);
            });

            var response = await navigator.Get<EchoResponse>("https://postman-echo.com/cookies");
            Assert.Equal(response.Cookies, values);
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
