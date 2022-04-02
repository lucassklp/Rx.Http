using Models;
using Models.Consumers;
using Rx.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Samples
{
    internal class Application
    {
        private readonly TheMovieDatabaseConsumer tmdbConsumer;
        private readonly JsonPlaceHolderConsumer jsonPlaceHolderConsumer;
        private RxHttpClient httpClient;

        public Application(TheMovieDatabaseConsumer tmdbConsumer, JsonPlaceHolderConsumer jsonPlaceHolderConsumer, RxHttpClient httpClient)
        {
            this.tmdbConsumer = tmdbConsumer;
            this.jsonPlaceHolderConsumer = jsonPlaceHolderConsumer;
            this.httpClient = httpClient;
        }

        public async Task Execute()
        {
            await httpClient.Get("http://google.com");

            await tmdbConsumer.ListMovies();
            await jsonPlaceHolderConsumer.GetTodos();
            await jsonPlaceHolderConsumer.SendPost(new Post()
            {
                Title = "Foo",
                Body = "Bar",
                UserId = 3
            });
        }
    }
}
