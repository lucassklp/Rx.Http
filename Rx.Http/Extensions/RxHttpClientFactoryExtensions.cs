using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Rx.Http.Extensions
{
    public static class RxHttpClientFactoryExtensions
    {
        public static IServiceCollection UseRxHttp(this IServiceCollection services)
        {
            services.AddHttpClient<RxHttpClient>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            return services;
        }

        public static IServiceCollection UseRxHttp(this IServiceCollection services, Action<IHttpClientBuilder> httpClientBuiderOptions)
        {
            var builder = services.AddHttpClient<RxHttpClient>();
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
            httpClientBuiderOptions.Invoke(builder);
            return services;
        }
    }
}