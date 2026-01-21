using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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
            var headersFormatted = JsonSerializer.Serialize(headers);
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
                    catch(Exception)
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
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}