using System;
using System.Net.Http;

namespace Rx.Http
{
    public abstract class RxConsumer : IDisposable
    {
        private readonly RxHttpClient http;
        private readonly IConsumerContext<RxConsumer> context;
        protected RxConsumer(IConsumerContext<RxConsumer> context)
        {
            this.context = context;
            http = new RxHttpClient(context.Http, context.Logger);
        }

        protected IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> options = null)
            where TResponse : class
        {
            return http.Get<TResponse>(url, ApplyOptions(options));
        }

        protected IObservable<HttpResponseMessage> Get(string url, Action<RxHttpRequestOptions> options = null)
        {
            return http.Get(url, ApplyOptions(options));
        }

        protected IObservable<TResponse> Post<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
            where TResponse : class
        {
            return http.Post<TResponse>(url, obj, ApplyOptions(options));
        }

        protected IObservable<HttpResponseMessage> Post(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return http.Post(url, obj, ApplyOptions(options));
        }

        protected IObservable<TResponse> Put<TResponse>(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
            where TResponse : class
        {
            return http.Put<TResponse>(url, obj, ApplyOptions(options));
        }
        protected IObservable<HttpResponseMessage> Put(string url, object obj = null, Action<RxHttpRequestOptions> options = null)
        {
            return http.Put(url, obj, ApplyOptions(options));
        }

        protected IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> options = null)
            where TResponse : class
        {
            return http.Delete<TResponse>(url, ApplyOptions(options));
        }

        protected IObservable<HttpResponseMessage> Delete(string url, Action<RxHttpRequestOptions> options = null)
        {
            return http.Delete(url, ApplyOptions(options));
        }

        private Action<RxHttpRequestOptions> ApplyOptions(Action<RxHttpRequestOptions> action) => (RxHttpRequestOptions options) =>
        {
            context.RequestInterceptors.ForEach(ri => options.AddRequestInteceptor(ri));
            context.ResponseInterceptors.ForEach(ri => options.AddResponseInterceptor(ri));
            action?.Invoke(options);
        };
        
        public void Dispose()
        {
            this.http.Dispose();
        }
    }
}