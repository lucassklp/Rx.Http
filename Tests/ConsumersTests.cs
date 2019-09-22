using Models.Consumers;
using System.Reactive.Linq;
using Xunit;

namespace Tests
{
    public class ConsumersTests
    {
        private Injector injector;
        public ConsumersTests()
        {
            injector = new Injector();
        }

        [Fact]
        public async void CheckIfRequestIsWorking()
        {
            var tmdbConsumer = this.injector.Get<TheMovieDatabaseConsumer>();
            var response = await tmdbConsumer.ListMovies();

            Assert.NotNull(response);
        }

    }
}
