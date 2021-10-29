using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Logging
{
    public class RxHttpDefaultLogger : HttpLoggerBase, RxHttpLogger
    {
        private readonly ILogger<RxHttpLogger> logger;
        public RxHttpDefaultLogger(ILogger<RxHttpLogger> logger)
        {
            this.logger = logger;
        }

        public async Task OnReceive(HttpResponseMessage httpResponse, string url, HttpMethod method, Guid requestId)
        {
            logger.LogInformation(GetResponseLog(httpResponse, method, url, requestId));
            logger.LogDebug(GetHeadersLog(httpResponse.Headers, method, url, requestId, LoggingMessageType.Response));
            logger.LogDebug(await GetBodyLog(httpResponse.Content, method, url, requestId, LoggingMessageType.Response));
        }

        public async Task OnSend(HttpRequestMessage httpRequest, Guid requestId)
        {
            logger.LogInformation(GetRequestLog(httpRequest, requestId));
            logger.LogDebug(GetHeadersLog(httpRequest.Headers, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, LoggingMessageType.Request));
            logger.LogDebug(await GetBodyLog(httpRequest.Content, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, LoggingMessageType.Request));
        }
    }
}
