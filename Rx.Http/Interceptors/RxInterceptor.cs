using Rx.Http.Requests;

namespace Rx.Http.Interceptors
{
    public interface RxInterceptor
    {
        void Intercept(RxHttpRequest request);
    }
}