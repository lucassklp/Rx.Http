using Microsoft.Extensions.Logging;
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
            this.Url = url;
            this.obj = obj;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
        }

        public RxPutHttpRequest(HttpClient http, ILogger logger, string url, object obj = null, Action<RxHttpRequestOptions> options = null) : base(http, logger)
        {
            this.Url = url;
            this.obj = obj;
            this.optionsCallback = options;
            this.QueryStrings = new Dictionary<string, string>();
        }

        internal override string MethodName { get; set; } = "PUT";

        internal override Task<HttpResponseMessage> DoRequest(string url, HttpContent content) => http.PutAsync(url, content);
    }
}
