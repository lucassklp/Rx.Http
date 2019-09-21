using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Rx.Http.Exceptions
{
    public class RxHttpRequestException : HttpRequestException
    {
        public HttpResponseMessage Response { get; private set; }

        public RxHttpRequestException(HttpResponseMessage response, Exception inner) : base(inner.Message, inner)
        {
            this.Response = response;
        }
    }
}
