using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
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
            RxHttpLogging logger = null) : base(http, url, requestInterceptors, responseInterceptors, options, logger)
        {
        }

        protected override Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content) => http.DeleteAsync(url);
    }
}
