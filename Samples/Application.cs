using Models;
using Rx.Http;
using Rx.Http.MediaTypes;
using Samples.Consumers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Samples
{
    internal class Application
    {
        private readonly TheMovieDatabaseConsumer tmdbConsumer;
        private readonly RxHttpClient httpClient;
        public Application(TheMovieDatabaseConsumer tmdbConsumer, RxHttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.tmdbConsumer = tmdbConsumer;
        }

        public async Task Execute()
        {
            while (true)
            {
                //Get the html code from the google home page
                await httpClient.Get("http://www.google.com");

                //Asynchronously, get the json from jsonplaceholder and serialize it. 
                httpClient.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/", options =>
                {
                    options.RequestMediaType = new JsonHttpMediaType();
                    options.AddHeader("Authorization", "Bearer <token>");
                    options.QueryStrings.Add("name", "John Doe");
                }).Subscribe();

                tmdbConsumer.ListMovies().Subscribe();

                httpClient.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts", new Post()
                {
                    Title = "Foo",
                    Body = "Bar",
                    UserId = 3
                }).Subscribe();

                await httpClient.Post(@"https://postman-echo.com/post");
            }
        }
    }
}
