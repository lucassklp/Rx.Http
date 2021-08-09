using Rx.Http.Interceptors;
using Rx.Http.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace Rx.Http
{
    public class ConsumerContext<T> : IConsumerContext<T>
    {
        public List<RxRequestInterceptor> RequestInterceptors { get; private set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; private set; }
        public HttpClient Http { get; private set; }
        public RxHttpLogger Logger { get; set; }

        public ConsumerContext(HttpClient http, RxHttpLogger logger = null)
        {
            RequestInterceptors = new List<RxRequestInterceptor>();
            ResponseInterceptors = new List<RxResponseInterceptor>();
            Http = http;
            Logger = logger;
        }
    }
}
