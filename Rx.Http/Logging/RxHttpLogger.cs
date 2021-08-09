using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rx.Http.Logging
{
    public interface RxHttpLogger
    {
        Task OnSend(HttpRequestMessage httpContent, Guid requestId);
        Task OnReceive(HttpResponseMessage httpResponse, string url, HttpMethod method, Guid requestId);
    }
}
