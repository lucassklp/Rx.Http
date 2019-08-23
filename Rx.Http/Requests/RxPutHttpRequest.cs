using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    class RxPutHttpRequest : RxHttpRequest
    {
        public RxPutHttpRequest(HttpClient http) : base(http)
        {
        }

        public RxPutHttpRequest(HttpClient http, ILogger logger) : base(http, logger)
        {

        }

        public override string MethodName { get; internal set; } = "PUT";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => http.PutAsync(url, content);
    }
}
