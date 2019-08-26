using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    public class RxGetHttpRequest : RxHttpRequest
    {
        public RxGetHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> options) : base(http)
        {
            this.Url = url;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
        }

        public RxGetHttpRequest(HttpClient http, ILogger logger, string url, Action<RxHttpRequestOptions> options = null) : base(http, logger)
        {
            this.optionsCallback = options;
            this.Url = url;
            this.QueryStrings = new Dictionary<string, string>();
        }

        internal override string MethodName { get; set; } = "GET";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => this.http.GetAsync(url);
    }
}