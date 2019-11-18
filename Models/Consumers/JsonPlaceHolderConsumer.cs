using Rx.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Consumers
{
    public class JsonPlaceHolderConsumer : RxConsumer
    {
        public JsonPlaceHolderConsumer(IConsumerConfiguration<JsonPlaceHolderConsumer> configuration) : base(configuration)
        {

        }

        public IObservable<List<Todo>> GetTodos() => Get<List<Todo>>("todos");
        public IObservable<Identifiable> SendPost(Post post) => Post<Identifiable>("posts", post);
    }
}
