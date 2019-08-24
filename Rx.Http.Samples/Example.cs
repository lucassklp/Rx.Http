using Microsoft.Extensions.Logging;
using Rx.Http.Samples.Consumers;
using Rx.Http.Samples.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Http.Samples
{
    class Example
    {
        private ILogger<RxHttpClient> logger;
        public Example(ILogger<RxHttpClient> logger)
        {
            this.logger = logger;
        }

        public async Task Execute()
        {
            RxHttpClient httpClient = new RxHttpClient(new HttpClient(), logger);

            //Get the html code from the google home page
            var response = await httpClient.Get("http://www.google.com");

            //Asynchronously, get the json from jsonplaceholder and serialize it. 
            httpClient.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens =>
            {
                Console.WriteLine("Json request finished!");
            });

            var tmdbConsumer = new TheMovieDatabaseConsumer();
            var item = await tmdbConsumer.ListMovies();

            Console.ReadKey();
        }
    }
}
