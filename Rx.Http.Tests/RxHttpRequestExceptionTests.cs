using Rx.Http;
using Rx.Http.Exceptions;
using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxHttpRequestExceptionTests
    {
        [Fact]
        public void TestConstructorExposesResponseAndInnerException()
        {
            var request = new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>());
            var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var response = new RxHttpResponse(httpResponse, request);
            var inner = new InvalidOperationException("Deserialization failed");

            var exception = new RxHttpRequestException(response, inner);

            Assert.Same(response, exception.Response);
            Assert.Same(inner, exception.InnerException);
            Assert.Equal(inner.Message, exception.Message);
        }
    }
}
