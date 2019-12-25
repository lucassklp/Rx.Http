using Rx.Http;
using System;

namespace Models.Consumers
{
    public class GoogleConsumer : RxConsumer
    {
        public GoogleConsumer(IConsumerConfiguration<GoogleConsumer> configuration) : base(configuration)
        {

        }

        public IObservable<string> GetGoogleContent() => Get("");
    }
}
