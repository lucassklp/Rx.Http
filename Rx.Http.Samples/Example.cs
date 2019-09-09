using Rx.Http.MediaTypes;
using Rx.Http.Samples.Consumers;
using Rx.Http.Tests.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Rx.Http.Samples
{
    class Example
    {
        private TheMovieDatabaseConsumer tmdbConsumer;
        private RxHttpClient httpClient;
        public Example(TheMovieDatabaseConsumer tmdbConsumer, RxHttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.tmdbConsumer = tmdbConsumer;
        }

        public void Execute()
        {
            int i = 0;
            while (true)
            {
                //Get the html code from the google home page
                httpClient.Get("http://www.google.com").Subscribe();

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

                httpClient.Post(@"https://postman-echo.com/post").Subscribe();

                i += 5;
                Console.WriteLine(i);
            }
        }
    }
}
