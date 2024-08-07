﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Models.Postman;
using Rx.Http;

namespace Models.Consumers
{
    public class PostmanConsumer : RxHttpClient
    {
        public PostmanConsumer(HttpClient httpClient) : base(httpClient, null)
        {
            httpClient.BaseAddress = new Uri("https://postman-echo.com");
        }

        public IObservable<EchoResponse> GetWithQueryString<T>(IDictionary<string, T> query)
        {
            return Get<EchoResponse>("get", opts =>
            {
                opts.AddQueryString(query);
            });
        }

        public IObservable<EchoResponse> GetWithHeaders<T>(IDictionary<string, T> headers)
        {
            return Get<EchoResponse>("get", opts =>
            {
                opts.AddHeader(headers);
            });
        }

        public IObservable<PostResponse<T>> Post<T>(T body)
        {
            return Post<PostResponse<T>>("post", body);
        }
    }
}
