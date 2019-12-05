#if NETSTANDARD2_0

using System;
using System.Collections.Generic;
using System.Text;
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

#endif