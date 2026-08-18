using Rx.Http;
using Rx.Http.MediaTypes;
using Rx.Http.Serializers;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxHttpDefaultsTests
    {
        [Fact]
        public void TestDefaultSerializableIsNativeJson()
        {
            Assert.IsType<NativeJsonSerializer>(RxHttp.Default.Serializable);
        }

        [Fact]
        public void TestDefaultRequestAndResponseMediaTypesAreJson()
        {
            Assert.IsType<JsonHttpMediaType>(RxHttp.Default.RequestMediaType);
            Assert.IsType<JsonHttpMediaType>(RxHttp.Default.ResponseMediaType);
        }
    }
}
