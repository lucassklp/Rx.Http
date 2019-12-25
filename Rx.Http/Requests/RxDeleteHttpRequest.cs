using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    internal class RxDeleteHttpRequest : RxHttpRequest
    {
        public RxDeleteHttpRequest(HttpClient http,
            string url,
            Action<RxHttpRequestOptions> options,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null,
            RxHttpLogging logger = null) : base(http, url, null, requestInterceptors, responseInterceptors, options, logger)
        {
        }

        protected override Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content) => Http.DeleteAsync(url);
    }
}
