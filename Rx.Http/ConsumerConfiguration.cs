using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public class ConsumerProvider<T> : IConsumerConfiguration<T>
    {
        public List<RxInterceptor> Interceptors { get; set; }
        public ConsumerProvider(HttpClient http)
        {
            Interceptors = new List<RxInterceptor>();
            Http = http;
        }

        public void AddInterceptors(params RxInterceptor[] interceptors)
        {
            Interceptors.AddRange(interceptors);
        }

        public HttpClient Http { get; private set; }
    }
}
