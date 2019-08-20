using Rx.Http.Interceptors;
using Rx.Http.Requests;

namespace Rx.Http.Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer()
        {

        }
        public override void Setup(RxHttpRequestConventions conventions)
        {
            conventions.BaseUrl = @"https://api.themoviedb.org/3/";
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            base.Setup(conventions);
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