using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Logging
{
    public class RxHttpConsoleLogger : HttpLoggerBase, RxHttpLogger
    {
        public async Task OnReceive(HttpResponseMessage httpResponse, string url, HttpMethod method, Guid requestId)
        {
            Console.WriteLine(GetResponseLog(httpResponse, method, url, requestId));
            Console.WriteLine(GetHeadersLog(httpResponse.Headers, method, url, requestId, LoggingMessageType.Response));
            Console.WriteLine(await GetBodyLog(httpResponse.Content, method, url, requestId, LoggingMessageType.Response));
        }

        public async Task OnSend(HttpRequestMessage httpRequest, Guid requestId)
        {
            Console.WriteLine(GetRequestLog(httpRequest, requestId));
            Console.WriteLine(GetHeadersLog(httpRequest.Headers, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, LoggingMessageType.Request));
            Console.WriteLine(await GetBodyLog(httpRequest.Content, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, LoggingMessageType.Request));
        }
    }
}