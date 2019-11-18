using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Rx.Http.Interceptors
{
    public interface RxResponseInterceptor
    {
        void Intercept(HttpResponseMessage response); 
    }
}
