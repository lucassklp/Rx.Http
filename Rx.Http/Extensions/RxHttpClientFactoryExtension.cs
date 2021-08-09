using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;

namespace Rx.Http.Extensions
{
    public static class RxHttpClientFactoryExtension
    {
        public static IServiceCollection UseRxHttp(this IServiceCollection services)
        {
            services.AddHttpClient<RxHttpClient>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>();
            services.AddScoped<TConsumer>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerContext<TConsumer>, ConsumerContext<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            return services;
        }
    }
}