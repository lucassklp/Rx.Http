using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Logging;

namespace Rx.Http.Extensions
{
    public static class RxHttpLoggingExtension
    {
        public static IServiceCollection AddRxHttpLogging<TLogging>(this IServiceCollection services)
            where TLogging : RxHttpLogger
        {
            services.AddScoped<RxHttpLogger, TLogging>();
            return services;
        }
    }
}
