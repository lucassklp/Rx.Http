using Rx.Http.Requests;
using System;
using System.Net.Http;


namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        private readonly HttpClient http;

        public Uri BaseAddress
        {
            get => http.BaseAddress;
            set => http.BaseAddress = value;
        }

        public RxHttpClient(HttpClient http)
        {
            this.http = http;
        }

        public IObservable<string> Get(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return CreateGetRequest(url, opts).Request();
        }

        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            return CreateGetRequest(url, opts).Request<TResponse>();
        }

        internal RxGetHttpRequest CreateGetRequest(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return new RxGetHttpRequest(http, url, opts);
        }

        public IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return CreatePostRequest(url, obj, options).Request<TResponse>();
        }

        public IObservable<string> Post(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePostRequest(url, obj, options).Request();
        }

        public IObservable<T> Post<T>(string url, HttpContent form, Action<RxHttpRequestOptions> options = null)
            where T : class
        {
            return CreatePostRequest(url, form, options).Request<T>();
        }

        public IObservable<string> Post(string url, FormUrlEncodedContent content, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePostRequest(url, content, options).Request();
        }

        internal RxPostHttpRequest CreatePostRequest(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return new RxPostHttpRequest(http, url, obj, options);
        }

        public IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return CreatePutRequest(url, obj, options).Request<TResponse>();
        }

        public IObservable<string> Put(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePutRequest(url, obj, options).Request();
        }

        public IObservable<T> Put<T>(string url, FormUrlEncodedContent form, Action<RxHttpRequestOptions> options = null)
            where T : class
        {
            return CreatePutRequest(url, form, options).Request<T>();
        }

        public IObservable<string> Put(string url, HttpContent content, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePutRequest(url, content, options).Request();
        }


        internal RxPutHttpRequest CreatePutRequest(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return new RxPutHttpRequest(http, url, obj, options);
        }

        public IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return CreateDeleteRequest(url, options).Request<TResponse>();
        }

        public IObservable<string> Delete(string url, Action<RxHttpRequestOptions> options = null)
        {
            return CreateDeleteRequest(url, options).Request();
        }

        internal RxDeleteHttpRequest CreateDeleteRequest(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return new RxDeleteHttpRequest(http, url, opts);
        }

        public void Dispose()
        {
            http.Dispose();
        }
    }
}