using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Models.Consumers;
using Rx.Http;
using Rx.Http.Extensions;
using Rx.Http.Logging;
using System;
using System.Threading.Tasks;

namespace Samples
{
    internal class Program
    {
        public async static Task Main(string[] args)
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
                config.AddDebug();
                config.AddConsole();
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null, LogLevel.Information);
                options.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Information);
            });

            //Add this line to make possible to inject RxHttpClient
            services.UseRxHttp();

            //Add this line to provide the Logger
            services.AddRxHttpLogger<RxHttpConsoleLogger>();

            services.AddConsumer<TheMovieDatabaseConsumer>(http =>
            {
                http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
            })

            .AddConsumer<JsonPlaceHolderConsumer>(http =>
            {
                http.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com/");
            })
            .AddTransient<Application>();
        }
    }
}