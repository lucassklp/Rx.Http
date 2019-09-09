using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    class RxDeleteHttpRequest : RxHttpRequest
    {
        public RxDeleteHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> options) : base(http)
        {
            this.Url = url;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
        }

        protected override string MethodName { get; set; } = "DELETE";

        protected override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.DeleteAsync(url);
    }
}
