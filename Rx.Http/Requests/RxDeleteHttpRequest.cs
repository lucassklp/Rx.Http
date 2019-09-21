using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    internal class RxDeleteHttpRequest : RxHttpRequest
    {
        public RxDeleteHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> options) : base(http)
        {
            Url = url;
            optionsCallback = options;
            QueryStrings = new Dictionary<string, string>();
        }

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.DeleteAsync(url);
    }
}
