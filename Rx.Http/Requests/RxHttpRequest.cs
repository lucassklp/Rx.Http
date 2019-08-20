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
        internal HttpMediaType mediaType {get;}
        public RxHttpRequest(string url, Action<RxHttpRequestOptions> opts = null, object obj = null)
        {
            this.mediaType = mediaType;
            this.QueryStrings = new Dictionary<string, string>();
        }

        internal abstract IObservable<TResponse> Execute<TResponse>(HttpClient http) where TResponse: class;
    }
}