using Microsoft.Extensions.Logging;
using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public abstract class RxConsumer
    {
        private List<RxInterceptor> interceptors;

        private RxHttpClient http;

        public RxConsumer(HttpClient http, ILogger logger)
        {
            this.http = new RxHttpClient(http, logger);
            Setup(new RxHttpRequestConventions());
        }


        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreateGetRequest(url, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request<TResponse>();
        }

        public IObservable<string> Get(string url, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreateGetRequest(url, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request();
        }


        public IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreatePostRequest(url, obj, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request<TResponse>();
        }

        public IObservable<string> Post(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreatePostRequest(url, obj, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request();
        }

        public IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreatePutRequest(url, obj, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request<TResponse>();
        }

        public IObservable<string> Put(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreatePutRequest(url, obj, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request();
        }

        public IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreateDeleteRequest(url, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request<TResponse>();
        }

        public IObservable<string> Delete(string url, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreateDeleteRequest(url, opts);
            this.interceptors.ForEach(x => x.Intercept(request));
            return request.Request();
        }

        public virtual void Setup(RxHttpRequestConventions conventions) => ApplyConventions(conventions);

        private void ApplyConventions(RxHttpRequestConventions conventions)
        {
            this.interceptors = conventions.Interceptors;
        }
    }
}