using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;

namespace Rx.Http.Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(ILogger<RxHttpClient> logger): base(new RxHttpClient(new HttpClient(), logger))
        {

        }
        public override RxHttpRequestConventions Setup()
        {
            var conventions = new RxHttpRequestConventions();
            conventions.BaseUrl = @"https://api.themoviedb.org/3/";
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            return conventions;
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