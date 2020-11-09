using Microsoft.Extensions.DependencyInjection;

namespace Rx.Http.Extensions
{
    public static class RxHttpLoggingExtension
    {
        public static IServiceCollection AddRxHttpLogging<TLogging>(this IServiceCollection services)
            where TLogging : RxHttpLogging
        {
            services.AddScoped<RxHttpLogging, TLogging>();
            return services;
        }
    }
}
