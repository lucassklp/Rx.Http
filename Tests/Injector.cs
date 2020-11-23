using Microsoft.Extensions.DependencyInjection;
using Models.Consumers;
using Rx.Http;
using Rx.Http.Extensions;

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
            services.AddConsumer<TheMovieDatabaseConsumer>();
            services.AddConsumer<PostmanConsumer>();
            services.AddTransient<ConsumersTests>();
        }

        public T Get<T>()
        {
            return this.serviceProvider.GetService<T>();
        }
    }
}
