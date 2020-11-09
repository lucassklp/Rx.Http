using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http
{
    public class RxHttpDefaultLogging : RxHttpLogging
    {
        public RxHttpDefaultLogging(ILogger<RxHttpLogging> logger) : base(logger)
        {
        }

        public override async Task OnReceive(HttpResponseMessage httpResponse, string url)
        {
            var headers = FormatJson2(httpResponse.Headers.ToDictionary(x => x.Key, x => x.Value));
            Logger.LogInformation($"Response Headers ({httpResponse.StatusCode} - {url}): \n{headers}");

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
            Logger.LogInformation($"Response Body ({httpResponse.StatusCode} - {url}): \n{content}");
        }

        public override async Task OnSend(HttpContent httpContent, string url)
        {
            var headers = FormatJson2(httpContent.Headers.ToDictionary(x => x.Key, x => x.Value));
            Logger.LogInformation($"Request Headers ({url}): \n{headers}");

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
            Logger.LogInformation($"Request Body ({url}): \n{content}");
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


        private string FormatJson2(object content)
        {
            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
    }
}
