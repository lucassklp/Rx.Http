using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;

namespace Rx.Http
{
    public abstract class RxConsumer
    {
        private List<RxRequestInterceptor> requestInterceptors;
        private List<RxResponseInterceptor> responseInterceptors;
        private RxHttpClient http;

        protected RxConsumer(IConsumerConfiguration<RxConsumer> configuration)
        {
            requestInterceptors = configuration.RequestInterceptors;
            responseInterceptors = configuration.ResponseInterceptors;
            http = new RxHttpClient(configuration.Http);
        }

        protected IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreateGetRequest(url, opts, requestInterceptors, responseInterceptors);
            return request.Request<TResponse>();
        }

        protected IObservable<string> Get(string url, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreateGetRequest(url, opts, requestInterceptors, responseInterceptors);
            return request.Request();
        }


        protected IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreatePostRequest(url, obj, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request<TResponse>();
        }

        protected IObservable<string> Post(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreatePostRequest(url, obj, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request();
        }

        protected IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreatePutRequest(url, obj, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request<TResponse>();
        }

        protected IObservable<string> Put(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreatePutRequest(url, obj, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request();
        }

        protected IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            var request = http.CreateDeleteRequest(url, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request<TResponse>();
        }

        protected IObservable<string> Delete(string url, Action<RxHttpRequestOptions> opts = null)
        {
            var request = http.CreateDeleteRequest(url, opts);
            requestInterceptors.ForEach(x => request.RequestInterceptors.Add(x));
            responseInterceptors.ForEach(x => request.ResponseInterceptors.Add(x));
            return request.Request();
        }
    }
}