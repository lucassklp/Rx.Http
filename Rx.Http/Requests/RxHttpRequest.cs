using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest
    {
        public string Url {get;set;}
        public Dictionary<string, string> QueryStrings{get;set;}
        public HttpHeaders Headers {get;set;}
        public IDeserializer Deserializer {get;set;}
        public ISerializer Serializer {get;set;}
        internal HttpClient http;
        public RxHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> opts = null, object obj = null)
        {
            this.Url = url;
            this.QueryStrings = new Dictionary<string, string>();

            this.http = http;
            Setup(opts);
        }

        private void Setup(Action<RxHttpRequestOptions> opts)
        {
            var options = new RxHttpRequestOptions(http.DefaultRequestHeaders);
            opts?.Invoke(options);

            this.Headers = options.Headers;
            this.QueryStrings = options.QueryStrings;
            this.Serializer = options.Serializer;
            this.Deserializer = options.Deserializer;
        }

        internal abstract IObservable<TResponse> Request<TResponse>() where TResponse: class;
        internal abstract IObservable<string> Request();
    }
}