using Microsoft.Net.Http.Headers;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Rx.Http
{
    public class RxHttpRequestOptions
    {
        public IHttpMediaType RequestMediaType { get; set; }
        public IHttpMediaType ResponseMediaType { get; set; }
        internal HttpHeaders Headers { get; private set; }
        public Dictionary<string, string> QueryStrings { get; private set; }

        public RxHttpRequestOptions(HttpHeaders headers, Dictionary<string, string> queryStrings)
        {
            Headers = headers;
            QueryStrings = queryStrings;
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public void UseBasicAuthorization(string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
        }
    }
}