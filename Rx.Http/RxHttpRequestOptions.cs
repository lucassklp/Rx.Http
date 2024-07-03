using Rx.Http.Interceptors;
using Rx.Http.MediaTypes.Abstractions;
using System.Collections.Generic;

namespace Rx.Http
{
    public abstract class RxHttpRequestOptions
    {
        public abstract RxHttpRequestOptions SetRequestMediaType(IHttpMediaTypeSerializer mediaType);

        public abstract RxHttpRequestOptions SetResponseMediaType(IHttpMediaTypeDeserializer mediaType);

        public abstract RxHttpRequestOptions AddResponseInterceptor(RxResponseInterceptor interceptor);

        public abstract RxHttpRequestOptions AddRequestInteceptor(RxRequestInterceptor interceptor);

        public abstract RxHttpRequestOptions AddHeader<T>(string key, T value);

        public abstract RxHttpRequestOptions AddHeader<T>(string key, IEnumerable<T> values);

        public abstract RxHttpRequestOptions AddHeader<T>(IEnumerable<KeyValuePair<string, T>> pairs);

        public abstract RxHttpRequestOptions AddHeader<T>(IEnumerable<KeyValuePair<string, List<T>>> pairs);

        public abstract RxHttpRequestOptions AddHeader(object obj);

        public abstract RxHttpRequestOptions AddQueryString<T>(string key, T value);
        
        public abstract RxHttpRequestOptions AddQueryString<T>(string key, IEnumerable<T> values);

        public abstract RxHttpRequestOptions AddQueryString<T>(IEnumerable<KeyValuePair<string, T>> pairs);

        public abstract RxHttpRequestOptions AddQueryString<T>(IEnumerable<KeyValuePair<string, List<T>>> pairs);

        public abstract RxHttpRequestOptions AddQueryString(object obj);

        public string Url { get; protected set; }
    }
}
