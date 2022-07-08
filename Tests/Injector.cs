using Microsoft.Extensions.DependencyInjection;
using Models.Consumers;
using Rx.Http.Extensions;
using Rx.Http.Logging;

namespace Rx.Http.Tests
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
            services.UseRxHttp();
            services.AddRxHttpLogger<RxHttpDefaultLogger>();
            services.AddSingleton<JsonPlaceHolderConsumer>();
            services.AddSingleton<PostmanConsumer>();
            services.AddSingleton<TheMovieDatabaseConsumer>();
        }

        public T Get<T>()
        {
            return this.serviceProvider.GetService<T>();
        }
    }
}
