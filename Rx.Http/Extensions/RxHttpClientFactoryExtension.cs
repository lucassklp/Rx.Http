using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Rx.Http.Extensions
{
    public static class RxHttpClientFactoryExtension
    {
        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>();
            services.AddScoped<TConsumer>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }
    }
}