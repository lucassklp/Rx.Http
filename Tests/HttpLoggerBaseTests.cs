using Rx.Http.Logging;
using Rx.Http.MediaTypes;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace Tests
{
    public class HttpLoggerBaseTests
    {
        private HttpLoggerBase loggerBase = new HttpLoggerBase();


        [Fact]
        async void TestIfBodyLogWorksWithNullContent()
        {
            var guid = Guid.NewGuid();
            var url = "http://google.com";
            
            var bodyLog = await loggerBase.GetBodyLog(null, HttpMethod.Get, url, guid, LoggingMessageType.Request);
            var expected = $"Request body for GET {url} [RequestId = {guid}]:\n"; ;
            
            Assert.Equal(expected, bodyLog);
        }

        [Fact]
        async void TestIfBodyLogWorksWithContent()
        {
            var guid = Guid.NewGuid();
            var url = "http://google.com";

            var message = "This is an example of string";
            byte[] byteArray = Encoding.UTF8.GetBytes(message);
            var memoryStream = new MemoryStream(byteArray);
            var content = new StreamContent(memoryStream);

            var bodyLog = await loggerBase.GetBodyLog(content, HttpMethod.Patch, url, guid, LoggingMessageType.Request);
            var expected = $"Request body for PATCH {url} [RequestId = {guid}]:\n{message}"; ;

            Assert.Equal(expected, bodyLog);
        }

        [Fact]
        async void TestIfBodyLogWorksWithWrongContentType()
        {
            var guid = Guid.NewGuid();
            var url = "http://google.com";

            var message = "This is an example of string";
            byte[] byteArray = Encoding.UTF8.GetBytes(message);
            var memoryStream = new MemoryStream(byteArray);
            var content = new StreamContent(memoryStream);
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.Application.Json);

            var bodyLog = await loggerBase.GetBodyLog(content, HttpMethod.Delete, url, guid, LoggingMessageType.Request);
            var expected = $"Request body for DELETE {url} [RequestId = {guid}]:\n{message}"; ;

            Assert.Equal(expected, bodyLog);
        }

        [Fact]
        void TestHeaders()
        {
            var url = "http://google.com";
            var guid = Guid.NewGuid();

            var content = new StreamContent(new MemoryStream());
            content.Headers.Clear();

            var expected = $"Request headers for PUT {url} [RequestId = {guid}]: \n{{}}";

            var headersLog = loggerBase.GetHeadersLog(content.Headers, HttpMethod.Put, url, guid, LoggingMessageType.Request);

            Assert.Equal(expected, headersLog);
        }

    }
}
