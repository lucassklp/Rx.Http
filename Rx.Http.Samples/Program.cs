using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Samples.Consumers;
using System.Net.Http;

namespace Rx.Http.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var example = serviceProvider.GetService<Example>();
            example.Execute();

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
                http.BaseAddress = new System.Uri(@"https://api.themoviedb.org/3/");
            })
            .AddTransient<Example>();
        }

    }
}