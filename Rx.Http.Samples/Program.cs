using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rx.Http.Samples.Models;
using Rx.Http.Serializers;
using System.Reactive.Linq;
using System.Net.Http;
using Rx.Http.Samples.Consumers;

namespace Rx.Http.Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var request = new RxHttpClient(new HttpClient());

            //Get the html code from the google home page
            // var response = await request.Get("http://www.google.com");
            // Console.WriteLine("Google request finished!");

            // //Asynchronously, get the json from jsonplaceholder and serialize it. 
            // request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
            //     Console.WriteLine("Json request finished!");
            // });

            var tmdbConsumer = new TheMovieDatabaseConsumer();
            var item = await tmdbConsumer.ListMovies();


            Console.WriteLine("Main thread finished!");
            //Console.ReadKey();
        }
    }
}