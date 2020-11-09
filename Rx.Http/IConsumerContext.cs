using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public interface IConsumerContext<out T>
    {
        List<RxRequestInterceptor> RequestInterceptors { get; }
        List<RxResponseInterceptor> ResponseInterceptors { get; }
        HttpClient Http { get; }
        RxHttpLogging Logger { get; }

    }
}
