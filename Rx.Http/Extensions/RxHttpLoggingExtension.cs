#if NETSTANDARD2_0

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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