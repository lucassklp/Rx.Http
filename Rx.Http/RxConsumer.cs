using System;

namespace Rx.Http
{
    public abstract class RxConsumer
    {
        private readonly RxHttpClient http;
        private readonly IConsumerConfiguration<RxConsumer> configuration;

        protected RxConsumer(IConsumerConfiguration<RxConsumer> configuration)
        {
            this.configuration = configuration;
            this.http = new RxHttpClient(configuration.Http, configuration.Logger);
        }

        protected IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            return http.Get<TResponse>(url, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<string> Get(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return http.Get(url, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            return http.Post<TResponse>(url, obj, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<string> Post(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            return http.Post(url, obj, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            return http.Put<TResponse>(url, obj, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }
        protected IObservable<string> Put(string url, object obj = null, Action<RxHttpRequestOptions> opts = null)
        {
            return http.Put(url, obj, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> opts = null)
            where TResponse : class
        {
            return http.Delete<TResponse>(url, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }

        protected IObservable<string> Delete(string url, Action<RxHttpRequestOptions> opts = null)
        {
            return http.Delete(url, opts, configuration.RequestInterceptors, configuration.ResponseInterceptors);
        }
    }
}