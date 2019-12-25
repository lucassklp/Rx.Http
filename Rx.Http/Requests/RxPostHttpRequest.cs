using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    internal class RxPostHttpRequest : RxHttpRequest
    {
        public RxPostHttpRequest(HttpClient http,
            string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null,
            RxHttpLogging logger = null) : base(http, url, content, requestInterceptors, responseInterceptors, options, logger)
        {
        }

        protected override Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content) => Http.PostAsync(url, content);
    }
}
