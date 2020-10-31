using Microsoft.Extensions.DependencyInjection;
using Models.Consumers;
using Rx.Http;
using Rx.Http.Extensions;
using System;

namespace Tests
{
    class Injector
    {
        private ServiceProvider serviceProvider;
        public Injector()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient<RxHttpClient>();
            services.AddRxHttpLogging<RxHttpDefaultLogging>();
            services.AddConsumer<TheMovieDatabaseConsumer>(http =>
            {
                http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
            })
            .AddTransient<ConsumersTests>();
        }

        public T Get<T>()
        {
            return this.serviceProvider.GetService<T>();
        }
    }
}
