using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Net.Http;

namespace Rx.Http.Serializers.Body
{
    public abstract class BodySerializer
    {
        internal ISerializer serializer;
        internal IDeserializer deserializer;
        public BodySerializer(ISerializer serializer, IDeserializer deserializer)
        {
            this.serializer = serializer;
            this.deserializer = deserializer;
        }

        public BodySerializer(ITwoWaysSerializable serializable)
        {
            serializer = serializable;
            deserializer = serializable;
        }

        public abstract HttpContent Serialize(object obj);

        public T Deserialize<T>(Stream stream) where T: class => deserializer.Deserialize<T>(stream);
    }
}