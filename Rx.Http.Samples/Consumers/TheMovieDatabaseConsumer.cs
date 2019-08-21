using System;
using System.Net.Http;
using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Rx.Http.Samples.Models;

namespace Rx.Http.Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(): base(new RxHttpClient(new HttpClient()))
        {

        }
        public override RxHttpRequestConventions Setup()
        {
            var conventions = new RxHttpRequestConventions();
            //conventions.BaseUrl = @"/";
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            return conventions;
        }

        public IObservable<Result> ListMovies()
        {
            return Get<Result>("https://api.themoviedb.org/3/movie/popular");
        }
    }

    public class TheMovieDatabaseInterceptor : RxInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.QueryStrings.Add("api_key", "key");
        }
    }
}