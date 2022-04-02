using Rx.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Models.Consumers
{
    public class JsonPlaceHolderConsumer : RxHttpClient
    {
        public JsonPlaceHolderConsumer(HttpClient httpClient): base(httpClient, null)
        {
            httpClient.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com/");
        }

        public IObservable<List<Todo>> GetTodos() => Get<List<Todo>>("todos");
        public IObservable<Identifiable> SendPost(Post post) => Post<Identifiable>("posts", post);
    }
}
