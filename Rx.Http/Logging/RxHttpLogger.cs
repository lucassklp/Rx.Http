using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Logging
{
    public abstract class RxHttpLogger
    {
        public abstract Task OnSend(HttpContent httpContent, string url);
        public abstract Task OnReceive(HttpResponseMessage httpResponse, string url);
    }
}
