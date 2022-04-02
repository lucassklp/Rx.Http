using Rx.Http;
using Rx.Http.Interceptors;
using System;
using System.Net.Http;

namespace Models.Consumers
{
    public class TheMovieDatabaseConsumer : RxHttpClient
    {
        public TheMovieDatabaseConsumer(HttpClient httpClient): base(httpClient, null)
        {
            httpClient.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
            RequestInterceptors.Add(new TheMovieDatabaseInterceptor());
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