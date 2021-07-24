using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;

namespace Rx.Http.Logging
{
    public class RxHttpConsoleLogger : RxHttpLogger
    {
        public override async Task OnReceive(HttpResponseMessage httpResponse, string url)
        {
            var headers = httpResponse.Headers.ToDictionary(x => x.Key, x => x.Value);
            var headersFormatted = JsonConvert.SerializeObject(headers, Formatting.Indented);
            Console.WriteLine($"Response: ({httpResponse.StatusCode} - {url})");
            Console.WriteLine($"Response Headers: {headersFormatted}");

            string content = string.Empty;
            if (httpResponse.Content.Headers.ContentType.MediaType == MediaType.Application.Json)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                content = FormatJson(json);
            }
            else
            {
                content = await httpResponse.Content.ReadAsStringAsync();
            }
            Console.WriteLine($"Response Body: {content}");
        }

        public override async Task OnSend(HttpContent httpContent, string url)
        {
            var headers = httpContent.Headers.ToDictionary(x => x.Key, x => x.Value);
            var headersFormatted = JsonConvert.SerializeObject(headers, Formatting.Indented);
            Console.WriteLine($"Request Headers ({url}): \n{headersFormatted}");

            string content = string.Empty;

            if (httpContent.Headers.ContentType.MediaType == MediaType.Application.Json)
            {
                var json = await httpContent.ReadAsStringAsync();
                content = FormatJson(json);
            }
            else
            {
                content = await httpContent.ReadAsStringAsync();
            }
            Console.WriteLine($"Request Body ({url}): \n{content}");
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