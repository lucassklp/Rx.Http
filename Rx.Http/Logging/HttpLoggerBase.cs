using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;

namespace Rx.Http.Logging
{
    public class HttpLoggerBase
    {
        public string GetRequestLog(HttpRequestMessage request, Guid requestId)
        {
            return $"{request.Method.Method} {request.RequestUri} has [RequestId = {requestId}]";
        }

        public string GetResponseLog(HttpResponseMessage response, HttpMethod method, string url, Guid requestId)
        {
            return $"{method.Method} {url} returned {(int)response.StatusCode} {response.StatusCode} [RequestId = {requestId}]";
        }

        public string GetHeadersLog(HttpHeaders httpHeaders, HttpMethod method, string url, Guid requestId, LoggingMessageType messageType)
        {
            var headers = httpHeaders.ToDictionary(x => x.Key, x => x.Value);
            var headersFormatted = JsonConvert.SerializeObject(headers, Formatting.Indented);
            return $"{messageType} headers for {method.Method} {url} [RequestId = {requestId}]: \n{headersFormatted}";
        }

        public async Task<string> GetBodyLog(HttpContent httpContent, HttpMethod method, string url, Guid requestId, LoggingMessageType messageType)
        {
            var explanation = $"{messageType} body for {method.Method} {url} [RequestId = {requestId}]:\n";
            if (httpContent != null)
            {
                var content = await httpContent?.ReadAsStringAsync();
                if (httpContent?.Headers?.ContentType?.MediaType == MediaType.Application.Json)
                {
                    try
                    {
                        return explanation + FormatJson(content);
                    }
                    catch
                    {
                        return explanation + content;
                    }

                }
                return explanation + content;
            }
            return explanation;
        }

        private string FormatJson(string content)
        {
            object parsedJson = JsonConvert.DeserializeObject(content);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }
    }
}