using Rx.Http.Exceptions;
using Xunit;

namespace Rx.Http.Tests
{
    public class RxInvalidHeaderParameterExceptionTests
    {
        [Fact]
        public void TestDefaultMessage()
        {
            var exception = new RxInvalidHeaderParameterException();

            Assert.Equal("The header key or value provided are invalid for http request.", exception.Message);
        }
    }
}
