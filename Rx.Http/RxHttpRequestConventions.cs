using System.Collections.Generic;
using Rx.Http.Interceptors;

namespace Rx.Http
{
    public class RxHttpRequestConventions
    {
        public string BaseUrl {get;set;}
        public List<RxInterceptor> Interceptors {get;set;}
    }
}