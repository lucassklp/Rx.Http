using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Requests
{
    internal class RxPutHttpRequest : RxHttpRequest
    {
        public RxPutHttpRequest(HttpClient http,
            string url,
            object obj = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null) : base(http, url, requestInterceptors, responseInterceptors, options)
        {
            this.obj = obj;
        }

        protected override Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content) => http.PutAsync(url, content);
    }
}
