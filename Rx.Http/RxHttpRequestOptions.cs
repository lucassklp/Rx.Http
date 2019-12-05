using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rx.Http
{
    public abstract class RxHttpRequestOptions
    {
        public abstract RxHttpRequestOptions SetRequestMediaType(IHttpMediaType mediaType);

        public abstract RxHttpRequestOptions SetResponseMediaType(IHttpMediaType mediaType);

        public abstract RxHttpRequestOptions AddResponseInterceptor(RxResponseInterceptor interceptor);

        public abstract RxHttpRequestOptions AddRequestInteceptor(RxRequestInterceptor interceptor);

        public abstract RxHttpRequestOptions AddHeader(string key, string value);

        public abstract RxHttpRequestOptions AddHeader(IEnumerable<KeyValuePair<string, string>> pairs);

        public abstract RxHttpRequestOptions AddHeader(object obj);

        public abstract RxHttpRequestOptions AddQueryString(string key, string value);

        public abstract RxHttpRequestOptions AddQueryString(IEnumerable<KeyValuePair<string, string>> pairs);

        public abstract RxHttpRequestOptions AddQueryString(object obj);

        public abstract RxHttpRequestOptions UseBasicAuthorization(string user, string key);

        public abstract RxHttpRequestOptions UseBearerAuthorization(string token);

        protected IList<KeyValuePair<string, string>> GetKeysByObject(object obj)
        {
            var keys = new List<KeyValuePair<string, string>>();

            var type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                keys.Add(new KeyValuePair<string, string>(field.Name, field.GetValue(obj).ToString()));
            }

            foreach (var property in type.GetProperties())
            {
                keys.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(obj).ToString()));
            }

            return keys;
        }
    }
}
