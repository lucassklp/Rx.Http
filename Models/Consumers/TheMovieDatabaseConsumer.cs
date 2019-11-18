using Rx.Http;
using Rx.Http.Interceptors;
using Rx.Http.Requests;
using System;

namespace Models.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerConfiguration<TheMovieDatabaseConsumer> config) : base(config)
        {
            config.RequestInterceptors.Add(new TheMovieDatabaseInterceptor());
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxRequestInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.QueryStrings.Add("api_key", "eb7b25db28349bd4eef1498a5be9842f");
        }
    }
}