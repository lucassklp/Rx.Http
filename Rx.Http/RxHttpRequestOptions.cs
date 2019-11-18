using Microsoft.Net.Http.Headers;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;

namespace Rx.Http
{
    public class RxHttpRequestOptions
    {
        public IHttpMediaType RequestMediaType { get; set; }
        public IHttpMediaType ResponseMediaType { get; set; }

        public List<RxRequestInterceptor> RequestInterceptors { get; set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; set; }

        internal HttpHeaders Headers { get; private set; }
        public Dictionary<string, string> QueryStrings { get; private set; }

        public RxHttpRequestOptions(HttpHeaders headers, Dictionary<string, string> queryStrings)
        {
            Headers = headers;
            QueryStrings = queryStrings;
            RequestInterceptors = new List<RxRequestInterceptor>();
            ResponseInterceptors = new List<RxResponseInterceptor>();
        }

        public RxHttpRequestOptions AddHeader(string key, string value)
        {
            Headers.Add(key, value);
            return this;
        }

        public RxHttpRequestOptions AddHeaders(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            pairs.ToList().ForEach(x => AddHeader(x.Key, x.Value));
            return this;
        }

        public RxHttpRequestOptions UseBasicAuthorization(string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
            return this;
        }

        public RxHttpRequestOptions UseBearerAuthorization(string token)
        {
            AddHeader(HeaderNames.Authorization, $"Bearer {token}");
            return this;
        }
    }
}