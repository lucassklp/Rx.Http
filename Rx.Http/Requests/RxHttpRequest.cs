using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
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

        public Uri GetUri()
        {
            UriBuilder builder = null;
            if(http?.BaseAddress != null)
            {
                builder = new UriBuilder(http.BaseAddress.AbsoluteUri + Url);
            }
            else
            {
                builder = new UriBuilder(Url);
            }

            var query = HttpUtility.ParseQueryString(builder.Query);
            
            foreach (var entry in QueryStrings)
            {
                query[entry.Key] = entry.Value;    
            }

            builder.Query = query.ToString();
            return builder.Uri;
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