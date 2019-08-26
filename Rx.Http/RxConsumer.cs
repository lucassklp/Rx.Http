using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;

namespace Rx.Http
{
    public abstract class RxConsumer
    {
        private List<RxInterceptor> interceptors;

        private RxHttpClient http;

        public RxConsumer(RxHttpClient http)
        {
            this.http = http;
            var conventions = Setup();
            if (conventions != null)
            {
                ApplyConventions(conventions);
            }
        }


        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> func = null)
            where TResponse : class
        {
            var request = http.CreateGetRequest(url);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request<TResponse>();
        }

        public IObservable<string> Get(string url)
        {
            var request = http.CreateGetRequest(url);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request();
        }

        public abstract RxHttpRequestConventions Setup();

        private void ApplyConventions(RxHttpRequestConventions conventions)
        {
            this.interceptors = conventions.Interceptors;
            if (!string.IsNullOrWhiteSpace(conventions.BaseUrl))
            {
                this.http.BaseAddress = new UriBuilder(conventions.BaseUrl).Uri;
            }
        }
    }
}