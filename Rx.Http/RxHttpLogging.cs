using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rx.Http
{
    public abstract class RxHttpLogging
    {
        protected readonly ILogger<RxHttpLogging> logger;

        protected RxHttpLogging(ILogger<RxHttpLogging> logger)
        {
            this.logger = logger;
        }

        public abstract Task OnSend(HttpContent httpContent);
        public abstract Task OnReceive(HttpResponseMessage httpResponse);
    }
}
