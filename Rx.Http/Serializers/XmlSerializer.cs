using Rx.Http.Serializers.Interfaces;
using System.IO;
using SystemXmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace Rx.Http.Serializers
{
    public class XmlSerializer : ITwoWaysSerializable
    {
        public T Deserialize<T>(Stream stream)
        {
            var serializer = new SystemXmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        public Stream Serialize<T>(T data)
            where T : class
        {
            var serializer = new SystemXmlSerializer(typeof(T));
            var stream = new MemoryStream();
            serializer.Serialize(stream, data);
            stream.Position = 0;
            return stream;
        }
    }
}