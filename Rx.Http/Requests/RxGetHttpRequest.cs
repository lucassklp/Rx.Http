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
            Url = url;
            optionsCallback = options;
            QueryStrings = new Dictionary<string, string>();
        }

        protected override string MethodName { get; set; } = "GET";

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.GetAsync(url);
    }
}