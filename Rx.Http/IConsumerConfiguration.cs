using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public interface IConsumerConfiguration<out T>
    {
        List<RxRequestInterceptor> RequestInterceptors { get; }
        List<RxResponseInterceptor> ResponseInterceptors { get; }
        HttpClient Http { get; }
        RxHttpLogging Logger { get; }

    }
}
