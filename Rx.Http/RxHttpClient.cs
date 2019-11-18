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

        public Uri BaseAddress
        {
            get => httpClient.BaseAddress;
            set => httpClient.BaseAddress = value;
        }

        public RxHttpClient(HttpClient http)
        {
            this.httpClient = http;
        }

        public IObservable<string> Get(string path,
            Action<RxHttpRequestOptions> opts = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreateGetRequest(path, opts, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<TResponse> Get<TResponse>(string path, 
            Action<RxHttpRequestOptions> opts = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where TResponse : class
        {
            return CreateGetRequest(path, opts, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        internal RxGetHttpRequest CreateGetRequest(
            string path, 
            Action<RxHttpRequestOptions> opts = null, 
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxGetHttpRequest(httpClient, path, opts, requestInterceptors, responseInterceptors);
        }

        public IObservable<TResponse> Post<TResponse>(
            string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null
            ) where TResponse : class
        {
            return CreatePostRequest(url, obj, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<string> Post(
            string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePostRequest(url, obj, options, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<T> Post<T>(string url, 
            HttpContent form, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where T : class
        {
            return CreatePostRequest(url, form, options, requestInterceptors, responseInterceptors).Request<T>();
        }

        public IObservable<string> Post(string url,
            FormUrlEncodedContent content, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePostRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }

        internal RxPostHttpRequest CreatePostRequest(
            string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxPostHttpRequest(httpClient, url, obj, options, requestInterceptors, responseInterceptors);
        }

        public IObservable<TResponse> Put<TResponse>(
            string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null) where TResponse : class
        {
            return CreatePutRequest(url, obj, options, requestInterceptors, responseInterceptors).Request<TResponse>();
        }

        public IObservable<string> Put(string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePutRequest(url, obj, options, requestInterceptors, responseInterceptors).Request();
        }

        public IObservable<T> Put<T>(string url, 
            FormUrlEncodedContent form, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
            where T : class
        {
            return CreatePutRequest(url, form, options, requestInterceptors, responseInterceptors).Request<T>();
        }

        public IObservable<string> Put(string url, 
            HttpContent content, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return CreatePutRequest(url, content, options, requestInterceptors, responseInterceptors).Request();
        }


        internal RxPutHttpRequest CreatePutRequest(
            string url, 
            object obj = null, 
            Action<RxHttpRequestOptions> options = null,
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxPutHttpRequest(httpClient, url, obj, options, requestInterceptors, responseInterceptors);
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

        internal RxDeleteHttpRequest CreateDeleteRequest(string url,
            Action<RxHttpRequestOptions> opts = null, 
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null)
        {
            return new RxDeleteHttpRequest(httpClient, url, opts, requestInterceptors, responseInterceptors);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}