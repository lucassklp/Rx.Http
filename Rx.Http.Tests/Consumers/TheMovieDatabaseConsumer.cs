using Microsoft.Extensions.Logging;
using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;
using System;

namespace Rx.Http.Tests.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(IConsumerConfiguration<TheMovieDatabaseConsumer> http, ILoggerFactory logger) : base(http, logger)
        {

        }
        public override void Setup(RxHttpRequestConventions conventions)
        {
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            base.Setup(conventions);
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