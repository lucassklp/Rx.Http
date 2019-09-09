using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Rx.Http.MediaTypes
{
    public class JsonHttpMediaType : IHttpMediaType
    {
        private ITwoWaysSerializable serializer;
        public JsonHttpMediaType()
        {
            serializer = new JsonSerializer();
        }

        public T Deserialize<T>(Stream stream)
            where T : class
        {
            return serializer.Deserialize<T>(stream);
        }

        public HttpContent Serialize(object obj)
        {
            var stream = this.serializer.Serialize(obj);
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.Application.Json);
            return content;
        }
    }
}
