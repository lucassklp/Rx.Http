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
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, RxHttpClientMessageHandlerBuilderFilter>());
            return services;
        }

        public static IServiceCollection UseRxHttp(this IServiceCollection services, Action<IHttpClientBuilder> httpClientBuiderOptions)
        {
            var builder = services.AddHttpClient<RxHttpClient>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, RxHttpClientMessageHandlerBuilderFilter>());
            httpClientBuiderOptions.Invoke(builder);
            return services;
        }
    }

    // Skips the additional handlers (e.g. the default HttpClientFactory logging handlers) that
    // Microsoft.Extensions.Http registers for every named/typed client, but only for RxHttpClient's
    // own handler chain, since RxHttpLogger already covers request/response logging for it.
    // Removing IHttpMessageHandlerBuilderFilter from the service collection would strip those
    // handlers from every other HttpClient registered in the same IServiceCollection.
    internal class RxHttpClientMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private static readonly string RxHttpClientName = nameof(RxHttpClient);

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return builder =>
            {
                next(builder);
                if (builder.Name == RxHttpClientName)
                {
                    builder.AdditionalHandlers.Clear();
                }
            };
        }
    }
}