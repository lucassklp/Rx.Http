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

        [Fact]
        public async void CheckSameRequestTwice()
        {
            var tmdbConsumer = this.injector.Get<TheMovieDatabaseConsumer>();
            var response1 = await tmdbConsumer.ListMovies();
            var response2 = await tmdbConsumer.ListMovies();

            Assert.NotNull(response1);
            Assert.NotNull(response2);
        }
    }
}
