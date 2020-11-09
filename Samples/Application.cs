using Models;
using Models.Consumers;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Samples
{
    internal class Application
    {
        private readonly TheMovieDatabaseConsumer tmdbConsumer;
        private readonly JsonPlaceHolderConsumer jsonPlaceHolderConsumer;

        public Application(TheMovieDatabaseConsumer tmdbConsumer, JsonPlaceHolderConsumer jsonPlaceHolderConsumer)
        {
            this.tmdbConsumer = tmdbConsumer;
            this.jsonPlaceHolderConsumer = jsonPlaceHolderConsumer;
        }

        public async Task Execute()
        {
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

