using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Rx.Http.MediaTypes
{
    public class TextHttpMediaType : IHttpMediaType
    {
        private readonly ITwoWaysSerializable serializer;
        public TextHttpMediaType()
        {
            serializer = new TextSerializer();
        }

        public T Deserialize<T>(Stream stream) where T : class
        {
            return serializer.Deserialize<T>(stream);
        }

        public HttpContent Serialize(object obj)
        {
            var content = new StreamContent(serializer.Serialize(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.Text.Plain);
            return content;

        }
    }
}