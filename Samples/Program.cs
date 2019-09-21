using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Rx.Http;
using Samples.Consumers;
using System;
using System.Threading.Tasks;

namespace Samples
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var application = serviceProvider.GetService<Application>();
            await application.Execute();
        }

        private static void ConfigureServices(ServiceCollection services)
        {

            services.AddLogging(config =>
            {
                config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                config.AddConsole(); // Log to console (colored !)
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Information /* min level */);
                options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Information /* min level */);
            });

            services.AddHttpClient<RxHttpClient>();

            services.AddConsumer<TheMovieDatabaseConsumer>(http =>
            {
                http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
            })
            .AddTransient<Application>();
        }

    }
}