using System.Net.Http;

namespace Rx.Http.MediaTypes.Abstractions
{
    public interface IHttpMediaTypeSerializer
    {
        HttpContent Serialize(object obj);
    }
}
