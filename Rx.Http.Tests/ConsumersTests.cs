using Models;
using Models.Consumers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rx.Http.Tests
{
    public class ConsumersTests
    {
        private readonly Injector injector;
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

            var queryStrings = new Dictionary<string, object>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" },
                { "Boolean", true },
                { "Int", 1 },
                { "Float", 1.8129f },
                { "Double", 1.8129d },
            };

            var headers = await postmanConsumer.GetWithQueryString(queryStrings);

            Assert.Equal(headers.Args, new Dictionary<string, string>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Characters", "*&�%6dbajs&@#chv73*(#Y" },
                { "Boolean", "true" },
                { "Int", "1" },
                { "Float", "1.8129" },
                { "Double", "1.8129" },
            });
        }

        [Fact]
        public async Task TestHeaders()
        {
            var postmanConsumer = injector.Get<PostmanConsumer>();

            var headers = new Dictionary<string, object>
            {
                { "Foo", "Bar" },
                { "User", "John Doe" },
                { "Boolean", true },
                { "Int", 1 },
                { "Float", 1.8129f },
                { "Double", 1.8129d },
            };

            var expected = new Dictionary<string, string>
            {
                { "foo", "Bar" },
                { "user", "John Doe" },
                { "boolean", "true" },
                { "int", "1" },
                { "float", "1.8129" },
                { "double", "1.8129" },
            };

            var response = await postmanConsumer.GetWithHeaders(headers);
            var valid = expected.All(pair => response.Headers.ContainsKey(pair.Key) && response.Headers[pair.Key] == pair.Value);
            Assert.True(valid);
        }
    }
}
