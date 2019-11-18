using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http
{
    public class RxHttpDefaultLogging : RxHttpLogging
    {
        public RxHttpDefaultLogging(ILogger<RxHttpLogging> logger) : base(logger)
        {
        }

        public override async Task OnReceive(HttpResponseMessage httpResponse)
        {
            if(httpResponse.Content.Headers.ContentType.MediaType == MediaTypes.MediaType.Application.Json)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var formatted = FormatJson(json);
                logger.LogInformation(formatted);
            }
            else
            {
                logger.LogInformation(await httpResponse.Content.ReadAsStringAsync());
            }
        }

        public override async Task OnSend(HttpContent httpContent)
        {
            if(httpContent.Headers.ContentType.MediaType == MediaTypes.MediaType.Application.Json)
            {
                var json = await httpContent.ReadAsStringAsync();
                var formatted = FormatJson(json);
                logger.LogInformation(formatted);
            }
            else
            {
                logger.LogInformation(await httpContent.ReadAsStringAsync());
            }
        }

        private string FormatJson(string content)
        {
            object parsedJson = JsonConvert.DeserializeObject(content);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
