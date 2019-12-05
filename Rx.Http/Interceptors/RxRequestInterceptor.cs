using Rx.Http.Requests;

namespace Rx.Http.Interceptors
{
    public interface RxRequestInterceptor
    {
        void Intercept(RxHttpRequestOptions request);
    }
}