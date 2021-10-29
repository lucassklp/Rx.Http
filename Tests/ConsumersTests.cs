using Models;
using Models.Consumers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
        public async Task CheckIfRequestIsWorking()
        {
            var tmdbConsumer = injector.Get<TheMovieDatabaseConsumer>();
            var response = await tmdbConsumer.ListMovies();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task CheckSameRequestTwice()
        {
            var tmdbConsumer = injector.Get<TheMovieDatabaseConsumer>();
            var response1 = await tmdbConsumer.ListMovies();
            var response2 = await tmdbConsumer.ListMovies();

            Assert.NotNull(response1);
            Assert.NotNull(response2);
        }

        [Fact]
        public async Task TestPostWithBody()
        {
            var postmanConsumer = injector.Get<PostmanConsumer>();

            var todo = new Todo
            {
                Id = 12,
                Title = "Testing with special characters: àáç~ã*+ü?<>!ºªª",
                IsCompleted = true,
                UserId = 20
            };
            var response = await postmanConsumer.Post(todo);

            Assert.True(response.Data.Equals(todo));
        }

        [Fact]
        public async Task TestQueryStrings()
        {
            var postmanConsumer = injector.Get<PostmanConsumer>();

            var queryStrings = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" }
            };

            var headers = await postmanConsumer.GetWithQueryString(queryStrings);

            Assert.Equal(headers.Args, queryStrings);
        }

        [Fact]
        public async Task TestHeaders()
        {
            var postmanConsumer = injector.Get<PostmanConsumer>();

            var headers = new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" }
            };

            var response = await postmanConsumer.GetWithHeaders(headers);
            var valid = headers.All(i => response.Headers[i.Key.ToLower()] == i.Value);
            Assert.True(valid);
        }
    }
}
