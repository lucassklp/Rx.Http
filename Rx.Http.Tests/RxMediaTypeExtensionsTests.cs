using Rx.Http;
using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes;
using System.Collections.Generic;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxMediaTypeExtensionsTests
    {
        [Fact]
        public void TestUseJsonMediaTypeSetsJsonRequestMediaType()
        {
            var request = new RxHttpRequest("https://example.com", new List<RxRequestInterceptor>(), new List<RxResponseInterceptor>());

            request.UseJsonMediaType();

            Assert.IsType<JsonHttpMediaType>(request.RequestMediaType);
        }
    }
}
