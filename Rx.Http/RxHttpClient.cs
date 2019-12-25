using Rx.Http.Interceptors;
using Rx.Http.Requests;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly RxHttpLogging logger;

        public RxHttpClient(HttpClient http, RxHttpLogging logger = null)
        {
            this.httpClient = http;
            this.logger = logger;
        }

        public IObservable<string> Get(string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreateGetRequest(url, options, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<TResponse> Get<TResponse>(string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where TResponse : class
        {
            return CreateGetRequest(url, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        private RxGetHttpRequest CreateGetRequest(
            string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxGetHttpRequest(httpClient, url, options, requestInterceptors, responseInterceptors, logger);
        }

        public IObservable<TResponse> Post<TResponse>(
            string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null
            ) where TResponse : class
        {
            return CreatePostRequest(url, content, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<TResponse> Post<TResponse>(string url,
            HttpContent content,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where TResponse : class
        {
            return CreatePostRequest(url, content, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<string> Post(string url,
            object content,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePostRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<string> Post(string url,
            HttpContent content,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePostRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }

        private RxPostHttpRequest CreatePostRequest(
            string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxPostHttpRequest(httpClient, url, content, options, requestInterceptors, responseInterceptors, logger);
        }

        public IObservable<TResponse> Put<TResponse>(
            string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null) where TResponse : class
        {
            return CreatePutRequest(url, content, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<TResponse> Put<TResponse>(string url,
            HttpContent content,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where TResponse : class
        {
            return CreatePutRequest(url, content, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<string> Put(string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePutRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<string> Put(string url,
            HttpContent content,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePutRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }

        private RxPutHttpRequest CreatePutRequest(
            string url,
            object content = null,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxPutHttpRequest(httpClient, url, content, options, requestInterceptors, responseInterceptors, logger);
        }

        public IObservable<TResponse> Delete<TResponse>(string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null) where TResponse : class
        {
            return CreateDeleteRequest(url, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<string> Delete(string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreateDeleteRequest(url, options, requestInterceptors, responseInterceptors).Request();
        }

        private RxDeleteHttpRequest CreateDeleteRequest(string url,
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxDeleteHttpRequest(httpClient, url, options, requestInterceptors, responseInterceptors, logger);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}