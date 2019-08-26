using Microsoft.Extensions.Logging;
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

        public RxPostHttpRequest(HttpClient http, ILogger logger, string url, object obj = null, Action<RxHttpRequestOptions> options = null) : base(http, logger)
        {
            this.Url = url;
            this.obj = obj;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        internal override string MethodName { get; set; } = "POST";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => http.PostAsync(url, content);
    }
}
