using System.IO;
using System.Text.Json;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public class NativeJsonSerializer : ITwoWaysSerializable
    {
        public T Deserialize<T>(Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream);
        }

        public Stream Serialize<T>(T data) where T : class
        {
            var json = JsonSerializer.Serialize(data);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
