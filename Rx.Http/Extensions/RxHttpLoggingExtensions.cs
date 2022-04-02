using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Logging;

namespace Rx.Http.Extensions
{
    public static class RxHttpLoggerExtensions
    {
        public static IServiceCollection AddRxHttpLogger<TLogging>(this IServiceCollection services)
            where TLogging : class, RxHttpLogger
        {
            services.AddScoped<RxHttpLogger, TLogging>();
            return services;
        }
    }
}
