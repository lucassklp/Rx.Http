using System.IO;
using System.Text.Json;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public class NativeJsonSerializer : ITwoWaysSerializable
    {
        public NativeJsonSerializer()
        {
        }

        public T Deserialize<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(text);
        }

        public Stream Serialize<T>(T data) where T : class
        {
            var s = JsonSerializer.Serialize(data);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
