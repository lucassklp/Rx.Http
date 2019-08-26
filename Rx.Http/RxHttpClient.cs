using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;

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

        public RxHttpClient(HttpClient http)
        {
            this.http = http;
            this.logger = null;
        }

        #region GET Methods
        public IObservable<string> Get(string url)
        {
            return this.CreateGetRequest(url).Request();
        }

        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> func = null)
            where TResponse : class
        {
            return CreateGetRequest(url, func)
                .Request<TResponse>();
        }

        internal RxHttpRequest CreateGetRequest(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return new RxGetHttpRequest(this.http, this.logger, url, opts);
        }
        #endregion

        public IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null) where TResponse : class
        {
            return new RxPostHttpRequest(http, logger, url, obj, options).Request<TResponse>();
        }
    }
}