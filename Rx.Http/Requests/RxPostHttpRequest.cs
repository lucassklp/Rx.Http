using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    class RxPostHttpRequest : RxHttpRequest
    {
        public RxPostHttpRequest(HttpClient http, string url, object obj = null, Action<RxHttpRequestOptions> options = null) : base(http)
        {
            Url = url;
            this.obj = obj;
            optionsCallback = options;
            QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.PostAsync(url, content);
    }
}
