using Rx.Http;
using Rx.Http.Interceptors;
using System;

namespace Models.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerContext<TheMovieDatabaseConsumer> config) : base(config)
        {
            config.RequestInterceptors.Add(new TheMovieDatabaseInterceptor());
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxRequestInterceptor
    {
        public void Intercept(RxHttpRequestOptions request)
        {
            request.AddQueryString("api_key", "eb7b25db28349bd4eef1498a5be9842f");
        }
    }
}