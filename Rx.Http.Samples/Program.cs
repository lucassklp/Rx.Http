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
            var response = await request.Get<string>("http://google.com", opt => {
                opt.Serializer = new TextSerializer();
            });


            request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
                Console.WriteLine(itens.ToString());
            });
            
            Console.WriteLine("Test finished");

            Console.ReadKey();
        }
    }
}