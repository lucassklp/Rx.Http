using Microsoft.Extensions.Logging;
using Rx.Http.MediaTypes;
using Rx.Http.Samples.Consumers;
using Rx.Http.Tests.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Http.Samples
{
    class Example
    {
        private TheMovieDatabaseConsumer tmdbConsumer;
        public Example(TheMovieDatabaseConsumer tmdbConsumer)
        {
            this.tmdbConsumer = tmdbConsumer;
        }

        public async Task Execute()
        {
            while(true)
            {
                using (var httpClient = new RxHttpClient(new HttpClient(), null))
                {
                    //Get the html code from the google home page
                    var response = await httpClient.Get("http://www.google.com");

                    //Asynchronously, get the json from jsonplaceholder and serialize it. 
                    httpClient.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/", options => 
                    {
                        options.RequestMediaType = new JsonHttpMediaType();
                        options.AddHeader("Authorization", "Bearer <token>");
                        options.QueryStrings.Add("name", "John Doe");
                    }).Subscribe(itens =>
                    {
                        Console.WriteLine("Json request finished!");
                    });

                    var item = await tmdbConsumer.ListMovies();

                    httpClient.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts", new Post()
                    {
                        Title = "Foo",
                        Body = "Bar",
                        UserId = 3
                    }).Subscribe();

                    var postmanRequest = await httpClient.Post(@"https://postman-echo.com/post");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
