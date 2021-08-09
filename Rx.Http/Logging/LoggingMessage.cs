using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;

namespace Rx.Http.Logging
{
    public class LoggingMessage
    {
        internal string GetRequestLog(HttpRequestMessage request, Guid requestId)
        {
            return $"{request.Method.Method} {request.RequestUri} has RequestId = {requestId}";
        }

        internal string GetResponseLog(HttpResponseMessage response, HttpMethod method, string url, Guid requestId)
        {
            return $"{method.Method} {url} returned {(int)response.StatusCode} {response.StatusCode} [RequestId = {requestId}]";
        }

        internal string GetHeadersLog(HttpHeaders httpHeaders, HttpMethod method, string url, Guid requestId, string operation)
        {
            var headers = httpHeaders.ToDictionary(x => x.Key, x => x.Value);
            var headersFormatted = JsonConvert.SerializeObject(headers, Formatting.Indented);
            return $"{operation} Headers for {method.Method} {url} [RequestId = {requestId}]: \n{headersFormatted}";
        }

        internal async Task<string> GetBodyLog(HttpContent httpContent, HttpMethod method, string url, Guid requestId, string operation)
        {
            var content = await httpContent.ReadAsStringAsync();
            var explanation = $"{operation} Body for {method.Method} {url} [RequestId = {requestId}]:\n";
            if (httpContent?.Headers?.ContentType?.MediaType == MediaType.Application.Json)
            {
                return explanation + FormatJson(content);
            }
            return explanation + content;
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