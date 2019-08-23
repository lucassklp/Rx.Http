using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Rx.Http.Requests;

namespace Rx.Http
{
    public class RxHttpClient
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

        public IObservable<string> Get(string url)
        {
            return this.CreateGetRequest(url).Request();
        }

        internal RxHttpRequest CreateGetRequest(string url)
        {
            return new RxGetHttpRequest(this.http, url);
        }
        
        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> func = null) 
            where TResponse: class
        {
            return CreateGetRequest(url, func)
                .Request<TResponse>();
        }

        internal RxHttpRequest CreateGetRequest(string url, Action<RxHttpRequestOptions> func = null)
        {
            return new RxGetHttpRequest(this.http, url, func);
        }

    }
}