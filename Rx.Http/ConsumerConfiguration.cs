using Rx.Http.Interceptors;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public class ConsumerProvider<T> : IConsumerConfiguration<T>
    {
        public List<RxRequestInterceptor> RequestInterceptors { get; private set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; private set; }
        public HttpClient Http { get; private set; }

        public ConsumerProvider(HttpClient http)
        {
            RequestInterceptors = new List<RxRequestInterceptor>();
            ResponseInterceptors = new List<RxResponseInterceptor>();
            Http = http;
        }
    }
}
