using System;
using System.Collections.Generic;
using System.Text;

namespace Rx.Http
{
    public abstract class RxHttpLogging
    {
        public abstract void OnSend();
        public abstract void OnReceive();
    }
}
