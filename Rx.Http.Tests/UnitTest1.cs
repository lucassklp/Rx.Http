using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using Rx.Http.Serializers;
using Rx.Http.Tests.Models;
using Xunit;

namespace Rx.Http.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var request = new RxHttpClient();
            var response = await request.Get<string>("http://google.com", opt => {
                opt.Serializer = new TextSerializer();
            });


            request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
                System.Console.WriteLine(itens.ToString());
            });

            Thread.Sleep(20000);
            Console.WriteLine("Test finished");
        }
    }
}