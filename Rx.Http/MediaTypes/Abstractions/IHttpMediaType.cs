using System;
namespace Rx.Http.MediaTypes.Abstractions
{
    public interface IHttpMediaType: IHttpMediaTypeSerializer, IHttpMediaTypeDeserializer
    {
    }
}
