using Rx.Http;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxHttpRequestTests
    {
        private RxHttpRequest NewRequest(string url = "https://example.com")
        {
            return new RxHttpRequest(url, new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>());
        }

        [Fact]
        public void TestConstructorSetsUrl()
        {
            var request = NewRequest("https://example.com/api");

            Assert.Equal("https://example.com/api", request.Url);
            Assert.Empty(request.Headers);
            Assert.Empty(request.QueryStrings);
            Assert.Null(request.Content);
        }

        [Fact]
        public void TestConstructorWithContentSetsContent()
        {
            var content = new { Foo = "Bar" };
            var request = new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>(), content);

            Assert.Same(content, request.Content);
        }

        [Fact]
        public void TestConstructorWithOptionsInvokesAction()
        {
            var request = new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>(), null, options =>
            {
                options.AddHeader("Foo", "Bar");
            });

            Assert.Equal(new[] { "Bar" }, request.Headers["Foo"]);
        }

        [Fact]
        public void TestAddHeaderSingleValue()
        {
            var request = NewRequest();

            request.AddHeader("Foo", "Bar");

            Assert.Equal(new[] { "Bar" }, request.Headers["Foo"]);
        }

        [Fact]
        public void TestAddHeaderMultipleValuesForSameKey()
        {
            var request = NewRequest();
            IEnumerable<string> values = new[] { "Bar", "Baz" };

            request.AddHeader("Foo", values);

            Assert.Equal(new[] { "Bar", "Baz" }, request.Headers["Foo"]);
        }

        [Fact]
        public void TestAddHeaderFromKeyValuePairs()
        {
            var request = NewRequest();
            var pairs = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John" }
            };

            request.AddHeader(pairs);

            Assert.Equal(new[] { "Bar" }, request.Headers["Foo"]);
            Assert.Equal(new[] { "John" }, request.Headers["User"]);
        }

        [Fact]
        public void TestAddHeaderFromObject()
        {
            var request = NewRequest();

            request.AddHeader(new { Foo = "Bar", User = "John" });

            Assert.Equal(new[] { "Bar" }, request.Headers["Foo"]);
            Assert.Equal(new[] { "John" }, request.Headers["User"]);
        }

        [Fact]
        public void TestAddQueryStringSingleValue()
        {
            var request = NewRequest();

            request.AddQueryString("Foo", "Bar");

            Assert.Equal(new[] { "Bar" }, request.QueryStrings["Foo"]);
        }

        [Fact]
        public void TestAddQueryStringFromObject()
        {
            var request = NewRequest();

            request.AddQueryString(new { Foo = "Bar" });

            Assert.Equal(new[] { "Bar" }, request.QueryStrings["Foo"]);
        }

        [Fact]
        public void TestAddQueryStringConvertsBoolToLowerCase()
        {
            var request = NewRequest();

            request.AddQueryString("IsActive", true);

            Assert.Equal(new[] { "true" }, request.QueryStrings["IsActive"]);
        }

        [Fact]
        public void TestAddQueryStringConvertsDoubleWithInvariantCulture()
        {
            var request = NewRequest();

            request.AddQueryString("Price", 1.5d);

            Assert.Equal(new[] { "1.5" }, request.QueryStrings["Price"]);
        }

        [Fact]
        public void TestAddQueryStringConvertsFloatWithInvariantCulture()
        {
            var request = NewRequest();

            request.AddQueryString("Price", 1.5f);

            Assert.Equal(new[] { "1.5" }, request.QueryStrings["Price"]);
        }

        [Fact]
        public void TestAddRequestInterceptorAddsToList()
        {
            var interceptors = new List<RxRequestInterceptor>();
            var request = new RxHttpRequest("https://example.com", interceptors, new List<RxResponseInterceptor>());
            var interceptor = new FakeRequestInterceptor();

            request.AddRequestInteceptor(interceptor);

            Assert.Contains(interceptor, interceptors);
        }

        [Fact]
        public void TestAddResponseInterceptorAddsToList()
        {
            var interceptors = new List<RxResponseInterceptor>();
            var request = new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), interceptors);
            var interceptor = new FakeResponseInterceptor();

            request.AddResponseInterceptor(interceptor);

            Assert.Contains(interceptor, interceptors);
        }

        [Fact]
        public void TestSetRequestMediaTypeOverridesDefault()
        {
            var request = NewRequest();
            var mediaType = new FakeMediaType();

            request.SetRequestMediaType(mediaType);

            Assert.Same(mediaType, request.RequestMediaType);
        }

        [Fact]
        public void TestSetResponseMediaTypeOverridesDefault()
        {
            var request = NewRequest();
            var mediaType = new FakeMediaType();

            request.SetResponseMediaType(mediaType);

            Assert.Same(mediaType, request.ResponseMediaType);
        }

        private class FakeRequestInterceptor : RxRequestInterceptor
        {
            public void Intercept(RxHttpRequestOptions request) { }
        }

        private class FakeResponseInterceptor : RxResponseInterceptor
        {
            public void Intercept(HttpResponseMessage response) { }
        }

        private class FakeMediaType : IHttpMediaType
        {
            public T Deserialize<T>(System.IO.Stream stream) => default;
            public HttpContent Serialize(object obj) => null;
        }
    }
}
