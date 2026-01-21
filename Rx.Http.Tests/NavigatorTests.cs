using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

            var response = await navigator.Get<JsonDocument>("https://postman-echo.com/cookies");
            var cookies = response.RootElement.GetProperty("cookies").Deserialize<Dictionary<string, string>>();
            Assert.True(cookies.ContainsKey("Foo"));
            Assert.True(cookies.ContainsKey("Key"));
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

            var response = await navigator.Get<JsonDocument>("https://postman-echo.com/cookies");
            var cookies = response.RootElement.GetProperty("cookies");
            
            var identity = cookies.GetProperty("IDENTITY").Deserialize<List<string>>();
            Assert.Contains(identity, x => x == values["IDENTITY"][0]);
            Assert.Contains(identity, x => x == values["IDENTITY"][1]);
            
            var identityLegacy = cookies.GetProperty("IDENTITY_LEGACY").Deserialize<List<string>>();
            Assert.Contains(identityLegacy, x => x == values["IDENTITY_LEGACY"][0]);
            Assert.Contains(identityLegacy, x => x == values["IDENTITY_LEGACY"][1]);
            Assert.Contains(identityLegacy, x => x == values["IDENTITY_LEGACY"][2]);
            Assert.Contains(identityLegacy, x => x == values["IDENTITY_LEGACY"][3]);
            
            var session = cookies.GetProperty("SESSION").Deserialize<List<string>>();
            Assert.Contains(session, x => x == values["SESSION"][0]);
            Assert.Contains(session, x => x == values["SESSION"][1]);
            Assert.Contains(session, x => x == values["SESSION"][2]);

            var sessionLegacy = cookies.GetProperty("SESSION_LEGACY").Deserialize<List<string>>();
            Assert.Contains(sessionLegacy, x => x == values["SESSION_LEGACY"][0]);
            Assert.Contains(sessionLegacy, x => x == values["SESSION_LEGACY"][1]);
            Assert.Contains(sessionLegacy, x => x == values["SESSION_LEGACY"][2]);
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
