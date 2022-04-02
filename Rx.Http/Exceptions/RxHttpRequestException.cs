using System;
using System.Net.Http;

namespace Rx.Http.Exceptions
{
    public class RxHttpRequestException : HttpRequestException
    {
        public RxHttpResponse Response { get; private set; }

        public RxHttpRequestException(RxHttpResponse response, Exception inner) : base(inner.Message, inner)
        {
            this.Response = response;
        }
    }
}
