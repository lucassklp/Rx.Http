using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public interface IConsumerConfiguration<out T>
    {
        List<RxInterceptor> Interceptors { get; set; }
        void AddInterceptors(params RxInterceptor[] interceptors);
        HttpClient Http { get; }
    }
}
