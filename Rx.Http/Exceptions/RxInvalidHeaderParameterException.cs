using System;

namespace Rx.Http.Exceptions
{
    public class RxInvalidHeaderParameterException : Exception
    {
        public RxInvalidHeaderParameterException() : base("The header key or value provided are invalid for http request.")
        {

        }
    }
}
