using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rx.Http.Requests
{
    public class RxGetHttpRequest : RxHttpRequest
    {
        public RxGetHttpRequest(HttpClient http) : base(http)
        {
        }

        public RxGetHttpRequest(HttpClient http, ILogger logger) : base(http, logger)
        {
        }

        public override string MethodName { get; internal set; } = "GET";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => this.http.GetAsync(url);
    }
}