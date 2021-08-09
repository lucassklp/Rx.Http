using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Logging
{
    public class RxHttpDefaultLogger : LoggingMessage, RxHttpLogger
    {
        private readonly ILogger<RxHttpLogger> logger;
        public RxHttpDefaultLogger(ILogger<RxHttpLogger> logger)
        {
            this.logger = logger;
        }

        public async Task OnReceive(HttpResponseMessage httpResponse, string url, HttpMethod method, Guid requestId)
        {
            var operation = "Response";
            logger.LogInformation(GetResponseLog(httpResponse, method, url, requestId));
            logger.LogDebug(GetHeadersLog(httpResponse.Headers, method, url, requestId, operation));
            logger.LogDebug(await GetBodyLog(httpResponse.Content, method, url, requestId, operation));
        }

        public async Task OnSend(HttpRequestMessage httpRequest, Guid requestId)
        {
            var operation = "Request";
            logger.LogInformation(GetRequestLog(httpRequest, requestId));
            logger.LogDebug(GetHeadersLog(httpRequest.Headers, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, operation));
            logger.LogDebug(await GetBodyLog(httpRequest.Content, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, operation));
        }


    }
}
