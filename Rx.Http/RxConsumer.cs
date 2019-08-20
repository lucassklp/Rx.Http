using System.Collections.Generic;
using Rx.Http.Interceptors;

namespace Rx.Http
{
    public abstract class RxConsumer
    {
        
        private List<RxInterceptor> interceptors;

        private RxHttpClient request;

        public RxConsumer()
        {
            request = new RxHttpClient();
        }

        public RxConsumer(RxHttpClient rxRequest)
        {
            request = rxRequest;
        }

        public virtual void Setup(RxHttpRequestConventions conventions)
        {

        }

        // private void ApplyInterceptors()
        // {
        //     interceptors.ForEach(x => x.Intercept(this.request));
        // }
    }
}