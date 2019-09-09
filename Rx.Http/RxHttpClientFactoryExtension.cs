#if NETSTANDARD2_0

using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Interceptors;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public static class RxHttpClientFactoryExtension
    {
        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerConfiguration<TConsumer>, ConsumerProvider<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IConsumerConfiguration<TConsumer>, ConsumerProvider<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }
    }
}

#endif