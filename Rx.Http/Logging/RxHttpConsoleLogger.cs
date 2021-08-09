using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rx.Http.MediaTypes;

namespace Rx.Http.Logging
{
    public class RxHttpConsoleLogger : LoggingMessage, RxHttpLogger
    {
        public async Task OnReceive(HttpResponseMessage httpResponse, string url, HttpMethod method, Guid requestId)
        {
            var operation = "Response";
            Console.WriteLine(GetResponseLog(httpResponse, method, url, requestId));
            Console.WriteLine(GetHeadersLog(httpResponse.Headers, method, url, requestId, operation));
            Console.WriteLine(await GetBodyLog(httpResponse.Content, method, url, requestId, operation));
        }

        public async Task OnSend(HttpRequestMessage httpRequest, Guid requestId)
        {
            var operation = "Request";
            Console.WriteLine(GetRequestLog(httpRequest, requestId));
            Console.WriteLine(GetHeadersLog(httpRequest.Headers, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, operation));
            Console.WriteLine(await GetBodyLog(httpRequest.Content, httpRequest.Method, httpRequest.RequestUri.AbsoluteUri, requestId, operation));
        }
    }
}