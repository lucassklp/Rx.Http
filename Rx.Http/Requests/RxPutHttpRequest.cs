using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    class RxPutHttpRequest : RxHttpRequest
    {
        public RxPutHttpRequest(HttpClient http, string url, object obj = null, Action<RxHttpRequestOptions> options = null) : base(http)
        {
            Url = url;
            this.obj = obj;
            optionsCallback = options;
            QueryStrings = new Dictionary<string, string>();
        }

        protected override string MethodName { get; set; } = "PUT";

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.PutAsync(url, content);
    }
}
