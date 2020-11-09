using System;
using System.IO;

namespace Rx.Http.MediaTypes.Abstractions
{
    public interface IHttpMediaTypeDeserializer
    {
        T Deserialize<T>(Stream stream);
    }
}
