#if NETSTANDARD2_0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Rx.Http
{
    public interface IContainer<out T>
    {
        HttpClient Http {get;set;}
    }

    public class Container<T> : IContainer<T>
    {
        public Container(HttpClient http)
        {
            Http = http;
        }
        public HttpClient Http { get; set; }
    }

    public static class RxHttpClientFactoryExtension
    {
        public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services, Action<HttpClient> configure)
            where TConsumer : RxConsumer
        {
            services.AddHttpClient<IContainer<TConsumer>, Container<TConsumer>>(configure);
            services.AddScoped<TConsumer>();
            return services;
        }
    }
}
#endif