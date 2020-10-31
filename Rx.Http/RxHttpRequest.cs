using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Rx.Http
{
    public class RxHttpRequest : RxHttpRequestOptions
    {
        public Dictionary<string, string> QueryStrings { get; private set; }
        public Dictionary<string, List<string>> Headers { get; private set; }

        public IHttpMediaTypeSerializer RequestMediaType { get; private set; }
        public IHttpMediaTypeDeserializer ResponseMediaType { get; set; }

        public List<RxRequestInterceptor> RequestInterceptors { get; private set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; private set; }

        public object Content { get; private set; }

        public string Url { get; private set; }

        public RxHttpRequest(string url)
        {
            Initialize();
            Url = url;
        }

        public RxHttpRequest(string url, object content)
        {
            Initialize();
            Url = url;
            Content = content;
        }

        public RxHttpRequest(string url, object content, Action<RxHttpRequestOptions> options)
        {
            Initialize();
            Url = url;
            Content = content;
            options.Invoke(this);
        }

        private void Initialize()
        {
            RequestInterceptors = new List<RxRequestInterceptor>();
            ResponseInterceptors = new List<RxResponseInterceptor>();
            QueryStrings = new Dictionary<string, string>();
            Headers = new Dictionary<string, List<string>>();
            RequestMediaType = RxDefaults.DefaultRequestMediaType;
            ResponseMediaType = RxDefaults.DefaultResponseMediaType;
        }

        internal HttpContent BuildContent()
        {
            HttpContent httpContent = null;

            if (Content != null)
            {
                if (Content is HttpContent)
                {
                    httpContent = Content as HttpContent;
                }
                else
                {
                    httpContent = RequestMediaType.Serialize(Content);
                }
            }

            return httpContent;
        }


        #region Options
        public override RxHttpRequestOptions AddHeader(string key, string value)
        {
            if (Headers.ContainsKey(key))
            {
                Headers[key].Add(value);
            }
            else
            {
                Headers.Add(key, new List<string> { value });
            }

            return this;
        }

        public override RxHttpRequestOptions AddHeader(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            pairs.ToList().ForEach(x => AddHeader(x.Key, x.Value));
            return this;
        }

        public override RxHttpRequestOptions AddHeader(object obj)
        {
            AddHeader(obj.ToDictionary());
            return this;
        }

        public override RxHttpRequestOptions AddQueryString(string key, string value)
        {
            this.QueryStrings.Add(key, value);
            return this;
        }

        public override RxHttpRequestOptions AddQueryString(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            pairs.ToList().ForEach(x => AddQueryString(x.Key, x.Value));
            return this;
        }

        public override RxHttpRequestOptions AddQueryString(object obj)
        {
            AddQueryString(obj.ToDictionary());
            return this;
        }

        public override RxHttpRequestOptions AddRequestInteceptor(RxRequestInterceptor interceptor)
        {
            this.RequestInterceptors.Add(interceptor);
            return this;
        }

        public override RxHttpRequestOptions AddResponseInterceptor(RxResponseInterceptor interceptor)
        {
            this.ResponseInterceptors.Add(interceptor);
            return this;
        }

        public override RxHttpRequestOptions SetRequestMediaType(IHttpMediaTypeSerializer mediaType)
        {
            this.RequestMediaType = mediaType;
            return this;
        }

        public override RxHttpRequestOptions SetResponseMediaType(IHttpMediaTypeDeserializer mediaType)
        {
            this.ResponseMediaType = mediaType;
            return this;
        }
        #endregion
    }
}