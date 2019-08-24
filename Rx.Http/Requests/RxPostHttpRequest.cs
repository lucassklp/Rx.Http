using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rx.Http.Requests
{
    class RxPostHttpRequest : RxHttpRequest
    {
        public RxPostHttpRequest(HttpClient http): base(http)
        {
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        public RxPostHttpRequest(HttpClient http, ILogger logger) : base(http, logger)
        {
        }

        internal override string MethodName { get; set; } = "POST";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => http.PostAsync(url, content);
    }
}
