using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rx.Http.Samples.Models;
using Rx.Http.Serializers;
using System.Reactive.Linq;

namespace Rx.Http.Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var request = new RxHttpRequest();
            var response = await request.Get<string>("http://google.com");
            Console.WriteLine("Google request finished!");


            request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
                Console.WriteLine("Json request finished!");
            });

            Console.WriteLine("Main thread finished!");
            Console.ReadKey();
        }
    }
}