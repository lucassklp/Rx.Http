using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rx.Http.Requests
{
    public class RxGetHttpRequest : RxHttpRequest
    {
        public RxGetHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> options) : base(http)
        {
        }

        public RxGetHttpRequest(HttpClient http, ILogger logger, string url, Action<RxHttpRequestOptions> options = null) : base(http, logger)
        {
            this.optionsCallback = options;
            this.Url = url;
        }

        internal override string MethodName { get; set; } = "GET";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => this.http.GetAsync(url);
    }
}