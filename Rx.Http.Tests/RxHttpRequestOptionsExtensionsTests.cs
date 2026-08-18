using Rx.Http;
using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxHttpRequestOptionsExtensionsTests
    {
        private RxHttpRequest NewRequest()
        {
            return new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>());
        }

        [Fact]
        public void TestUseBasicAuthorizationAddsBase64EncodedHeader()
        {
            var request = NewRequest();

            request.UseBasicAuthorization("user", "pass");

            var expectedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("user:pass"));
            Assert.Equal(new[] { $"Basic {expectedToken}" }, request.Headers["Authorization"]);
        }

        [Fact]
        public void TestUseBearerAuthorizationAddsBearerHeader()
        {
            var request = NewRequest();

            request.UseBearerAuthorization("my-token");

            Assert.Equal(new[] { "Bearer my-token" }, request.Headers["Authorization"]);
        }
    }
}
