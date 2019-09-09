using System.IO;
using System.Net.Http;

namespace Rx.Http.MediaTypes.Abstractions
{
    public interface IHttpMediaType
    {
        T Deserialize<T>(Stream stream) where T : class;
        HttpContent Serialize(object obj);
    }
}
