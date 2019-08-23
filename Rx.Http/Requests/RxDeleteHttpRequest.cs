using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    class RxDeleteHttpRequest : RxHttpRequest
    {
        public RxDeleteHttpRequest(HttpClient http) : base(http)
        {

        }

        public RxDeleteHttpRequest(HttpClient http, ILogger logger) : base(http, logger)
        {

        }

        public override string MethodName { get; internal set; } = "DELETE";

        internal override Task<HttpResponseMessage> HttpMethod(string url, HttpContent content) => http.DeleteAsync(url);
    }
}
