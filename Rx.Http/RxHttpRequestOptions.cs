using Rx.Http.Serializers.Interfaces;
using System.Net.Http.Headers;
using System;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public class RxHttpRequestOptions
    {
        public ISerializer Serializer {get;set;}
        public IDeserializer Deserializer {get;set;}
        internal HttpHeaders Headers {get;set;}
        public Dictionary<string, string> QueryStrings {get; private set;}

        public RxHttpRequestOptions()
        {
            this.QueryStrings = new Dictionary<string, string>();
        }

        public void AddHeader(string key, string value)
        {
            this.Headers.Add(key, value);
        }

        public void UseBasicAuthorization(string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            this.AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
        }

        internal void ConfigureRequest(RxHttpClient request)
        {
            //request
        }
    }
}