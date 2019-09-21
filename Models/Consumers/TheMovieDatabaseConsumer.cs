using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Models;
using System;
using Rx.Http;

namespace Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerConfiguration<TheMovieDatabaseConsumer> config) : base(config)
        {
            config.AddInterceptors(new TheMovieDatabaseInterceptor());
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