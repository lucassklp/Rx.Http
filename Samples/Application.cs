using Models;
using Models.Consumers;
using Rx.Http;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Samples
{
    internal class Application
    {
        private readonly TheMovieDatabaseConsumer tmdbConsumer;
        private readonly GoogleConsumer googleConsumer;
        private readonly JsonPlaceHolderConsumer jsonPlaceHolderConsumer;

        private readonly RxHttpClient httpClient;
        public Application(TheMovieDatabaseConsumer tmdbConsumer, GoogleConsumer googleConsumer, JsonPlaceHolderConsumer jsonPlaceHolderConsumer, RxHttpClient httpClient)
        {
            this.tmdbConsumer = tmdbConsumer;
            this.googleConsumer = googleConsumer;
            this.jsonPlaceHolderConsumer = jsonPlaceHolderConsumer;
            this.httpClient = httpClient;
        }

        public async Task Execute()
        {
            while (true)
            {

                tmdbConsumer.ListMovies().Subscribe();

                jsonPlaceHolderConsumer.GetTodos().Subscribe();
                jsonPlaceHolderConsumer.SendPost(new Post()
                {
                    Title = "Foo",
                    Body = "Bar",
                    UserId = 3
                }).Subscribe();
            }
        }
    }
}

