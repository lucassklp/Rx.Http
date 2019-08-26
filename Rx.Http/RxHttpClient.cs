using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;

namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        private HttpClient http;
        private ILogger logger;
        public Uri BaseAddress
        {
            get => http.BaseAddress;
            set => http.BaseAddress = value;
        }

        public RxHttpClient(HttpClient http, ILogger<RxHttpClient> logger)
        {
            this.http = http;
            this.logger = logger;
        }

        public RxHttpClient(HttpClient http)
        {
            this.http = http;
            this.logger = null;
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
            return new RxGetHttpRequest(this.http, this.logger, url, opts);
        }

        public IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return CreatePostRequest(url, obj, options).Request<TResponse>();
        }

        public IObservable<string> Post(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePostRequest(url, obj, options).Request();
        }

        internal RxPostHttpRequest CreatePostRequest(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return new RxPostHttpRequest(http, logger, url, obj, options);
        }

        public IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return CreatePutRequest(url, obj, options).Request<TResponse>();
        }

        public IObservable<string> Put(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return CreatePutRequest(url, obj, options).Request();
        }

        internal RxPutHttpRequest CreatePutRequest(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return new RxPutHttpRequest(http, logger, url, obj, options);
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
            return new RxDeleteHttpRequest(this.http, this.logger, url, opts);
        }

        public void Dispose()
        {
            this.http.Dispose();
        }
    }
}