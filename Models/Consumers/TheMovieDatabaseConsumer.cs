using Rx.Http;
using Rx.Http.Interceptors;
using System;

namespace Models.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerContext<TheMovieDatabaseConsumer> context)
            : base(context)
        {
            context.Http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
            context.RequestInterceptors.Add(new TheMovieDatabaseInterceptor());
        }

        public IObservable<Movies> ListMovies() => Get<Movies>("movie/popular");
    }

    internal class TheMovieDatabaseInterceptor : RxRequestInterceptor
    {
        public void Intercept(RxHttpRequestOptions request)
        {
            request.AddQueryString("api_key", "eb7b25db28349bd4eef1498a5be9842f");
        }
    }
}