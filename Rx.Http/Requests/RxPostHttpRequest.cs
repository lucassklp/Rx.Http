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
            this.Url = url;
            this.obj = obj;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }


        protected override string MethodName { get; set; } = "POST";

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.PostAsync(url, content);
    }
}
