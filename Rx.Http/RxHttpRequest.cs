using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace Rx.Http
{
    public class RxHttpRequest : RxHttpRequestOptions
    {
        public ListDictionary<string, string> QueryStrings { get; private set; }
        public ListDictionary<string, string> Headers { get; private set; }

        public IHttpMediaTypeSerializer RequestMediaType { get; private set; }
        public IHttpMediaTypeDeserializer ResponseMediaType { get; set; }

        public List<RxRequestInterceptor> RequestInterceptors { get; private set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; private set; }

        public object Content { get; private set; }

        public RxHttpRequest(string url, List<RxRequestInterceptor> requestInterceptors, List<RxResponseInterceptor> responseInterceptors)
        {
            Initialize(requestInterceptors, responseInterceptors);
            Url = url;
        }

        public RxHttpRequest(string url, List<RxRequestInterceptor> requestInterceptors, List<RxResponseInterceptor> responseInterceptors, object content)
        {
            Initialize(requestInterceptors, responseInterceptors);
            Url = url;
            Content = content;
        }

        public RxHttpRequest(string url, List<RxRequestInterceptor> requestInterceptors, List<RxResponseInterceptor> responseInterceptors, object content, Action<RxHttpRequestOptions> options)
        {
            Initialize(requestInterceptors, responseInterceptors);
            Url = url;
            Content = content;
            options.Invoke(this);
        }

        private void Initialize(List<RxRequestInterceptor> requestInterceptors, List<RxResponseInterceptor> responseInterceptors)
        {
            RequestInterceptors = requestInterceptors;
            ResponseInterceptors = responseInterceptors;
            QueryStrings = new ListDictionary<string, string>();
            Headers = new ListDictionary<string, string>();
            RequestMediaType = RxHttp.Default.RequestMediaType;
            ResponseMediaType = RxHttp.Default.ResponseMediaType;
        }

        internal HttpContent BuildContent()
        {
            HttpContent httpContent = null;

            if (Content != null)
            {
                if (Content is HttpContent content)
                {
                    httpContent = content;
                }
                else
                {
                    httpContent = RequestMediaType.Serialize(Content);
                }
            }

            return httpContent;
        }


        #region Options
        public override RxHttpRequestOptions AddHeader<T>(string key, T value)
        {
            Headers.Append(key, ConvertToString(value));
            return this;
        }

        public override RxHttpRequestOptions AddHeader<T>(string key, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                AddHeader(key, value);
            }
            return this;
        }

        public override RxHttpRequestOptions AddHeader<T>(IEnumerable<KeyValuePair<string, T>> pairs)
        {
            foreach (var item in pairs)
            {
                AddHeader(item.Key, item.Value);
            }
            return this;
        }

        public override RxHttpRequestOptions AddHeader<T>(IEnumerable<KeyValuePair<string, List<T>>> pairs)
        {
            foreach (var pair in pairs)
            {
                foreach (var value in pair.Value)
                {
                    AddHeader(pair.Key, value);
                }
            }

            return this;
        }


        public override RxHttpRequestOptions AddHeader(object obj)
        {
            AddHeader(obj.ToDictionary());
            return this;
        }

        public override RxHttpRequestOptions AddQueryString<T>(string key, T value)
        {
            QueryStrings.Append(key, ConvertToString(value));
            return this;
        }

        public override RxHttpRequestOptions AddQueryString<T>(IEnumerable<KeyValuePair<string, T>> pairs)
        {
            foreach (var item in pairs)
            {
                AddQueryString(item.Key, item.Value);
            }
            return this;
        }

        public override RxHttpRequestOptions AddQueryString<T>(IEnumerable<KeyValuePair<string, List<T>>> pairs)
        {
            foreach (var pair in pairs)
            {
                foreach (var value in pair.Value)
                {
                    AddQueryString(pair.Key, value);
                }
            }

            return this;
        }

        public override RxHttpRequestOptions AddQueryString<T>(string key, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                AddQueryString(key, value);
            }
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

        private string ConvertToString<T>(T value)
        {
            if(value is bool)
            {
                return value?.ToString()?.ToLower();
            }

            if(value is double asDouble)
            {
                return asDouble.ToString(CultureInfo.InvariantCulture);
            }

            if(value is float asFloat)
            {
                return asFloat.ToString(CultureInfo.InvariantCulture);
            }

            return value?.ToString();
        }
        #endregion
    }
}