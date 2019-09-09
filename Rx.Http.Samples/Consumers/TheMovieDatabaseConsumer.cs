using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;
using System;

namespace Rx.Http.Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerConfiguration<TheMovieDatabaseConsumer> container) : base(container)
        {
            container.AddInterceptors(new TheMovieDatabaseInterceptor());
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.QueryStrings.Add("api_key", "key");
        }
    }
}