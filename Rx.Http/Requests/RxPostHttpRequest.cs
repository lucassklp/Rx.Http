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
            object obj = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null,
            RxHttpLogging logger = null) : base(http, url, requestInterceptors, responseInterceptors, options, logger)
        {
            this.obj = obj;
        }

        protected override Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content) => http.PostAsync(url, content);
    }
}
