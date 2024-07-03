using Models.Consumers;
using Rx.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class InjectionTests
    {
        private Injector injector;
        public InjectionTests()
        {
            injector = new Injector();
        }

        [Fact]
        public void CheckIfConsumerIsInjected()
        {
            var tmdbConsumer = this.injector.Get<TheMovieDatabaseConsumer>();

            Assert.NotNull(tmdbConsumer);
        }

        [Fact]
        public void CheckIfRxHttpClientIsInjected()
        {
            var http = this.injector.Get<RxHttpClient>();

            Assert.NotNull(http);
        }
    }
}
