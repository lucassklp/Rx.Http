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
        private readonly GoogleConsumer googleConsumer;
        private readonly JsonPlaceHolderConsumer jsonPlaceHolderConsumer;
        public Application(TheMovieDatabaseConsumer tmdbConsumer, GoogleConsumer googleConsumer, JsonPlaceHolderConsumer jsonPlaceHolderConsumer)
        {
            this.tmdbConsumer = tmdbConsumer;
            this.googleConsumer = googleConsumer;
            this.jsonPlaceHolderConsumer = jsonPlaceHolderConsumer;
        }

        public async Task Execute()
        {
            while (true)
            {
                googleConsumer.GetGoogleContent().Subscribe();
                tmdbConsumer.ListMovies().Subscribe();

                jsonPlaceHolderConsumer.GetTodos().Subscribe();
                jsonPlaceHolderConsumer.SendPost(new Post()
                {
                    Title = "Foo",
                    Body = "Bar",
                    UserId = 3
                }).Subscribe();

                //httpClient.Post(@"https://postman-echo.com/post").Subscribe();
            }
        }
    }
}

