﻿using Rx.Http.Interceptors;
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

        public abstract RxHttpRequestOptions AddHeader(string key, string value);

        public abstract RxHttpRequestOptions AddHeader(IEnumerable<KeyValuePair<string, string>> pairs);

        public abstract RxHttpRequestOptions AddHeader(object obj);

        public abstract RxHttpRequestOptions AddQueryString(string key, string value);

        public abstract RxHttpRequestOptions AddQueryString(IEnumerable<KeyValuePair<string, string>> pairs);

        public abstract RxHttpRequestOptions AddQueryString(IEnumerable<KeyValuePair<string, List<string>>> pairs);

        public abstract RxHttpRequestOptions AddQueryString(object obj);

        public string Url { get; protected set; }
    }
}
