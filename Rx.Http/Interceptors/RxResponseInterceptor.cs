using System.Net.Http;

namespace Rx.Http.Interceptors
{
    public interface RxResponseInterceptor
    {
        void Intercept(HttpResponseMessage response);
    }
}
