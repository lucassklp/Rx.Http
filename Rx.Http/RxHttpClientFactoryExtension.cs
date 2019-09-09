using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public interface IConsumerConfiguration<out T>
    {
        List<RxInterceptor> Interceptors { get; set; }
        void AddInterceptors(params RxInterceptor[] interceptors);
        HttpClient Http { get; }
    }

    public class ConsumerConfiguration<T> : IConsumerConfiguration<T>
    {
        public List<RxInterceptor> Interceptors { get; set; }
        public ConsumerConfiguration(HttpClient http)
        {
            Interceptors = new List<RxInterceptor>();
            Http = http;
        }

        public void AddInterceptors(params RxInterceptor[] interceptors)
        {
            this.Interceptors.AddRange(interceptors);
        }

        public HttpClient Http { get; private set; }
    }

    public static class RxHttpClientFactoryExtension
    {
        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerConfiguration<TConsumer>, ConsumerConfiguration<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }
    }
}
