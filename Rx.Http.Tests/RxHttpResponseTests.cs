using Rx.Http;
using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxHttpResponseTests
    {
        private RxHttpRequest NewRequest()
        {
            return new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>());
        }

        [Fact]
        public void TestPropertiesReflectUnderlyingHttpResponseMessage()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                ReasonPhrase = "Created",
                Content = new StringContent("Hello")
            };
            var request = NewRequest();

            var response = new RxHttpResponse(httpResponse, request);

            Assert.Same(request, response.Request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Created", response.ReasonPhrase);
            Assert.Same(httpResponse.Content, response.Content);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public void TestIsSuccessStatusCodeFalseForErrorStatus()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
            var response = new RxHttpResponse(httpResponse, NewRequest());

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public void TestEnsureSuccessStatusCodeThrowsForErrorStatus()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var response = new RxHttpResponse(httpResponse, NewRequest());

            Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
        }

        [Fact]
        public void TestDisposeDoesNotThrow()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Hello")
            };
            var response = new RxHttpResponse(httpResponse, NewRequest());

            response.Dispose();
            response.Dispose();
        }
    }
}
