using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Net.Http;

namespace Rx.Http.Serializers.Body
{
    public abstract class BodySerializer
    {
        public ISerializer Serializer;
        public IDeserializer Deserializer;
        public BodySerializer(ISerializer serializer, IDeserializer deserializer)
        {
            this.Serializer = serializer;
            this.Deserializer = deserializer;
        }

        public BodySerializer(ITwoWaysSerializable serializable)
        {
            Serializer = serializable;
            Deserializer = serializable;
        }

        public abstract HttpContent Serialize(object obj);

        public T Deserialize<T>(Stream stream) where T : class => Deserializer.Deserialize<T>(stream);
    }
}