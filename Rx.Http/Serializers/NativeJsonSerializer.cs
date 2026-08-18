using System.IO;
using System.Text.Json;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public class NativeJsonSerializer : ITwoWaysSerializable
    {
        // Case-insensitive to tolerate the camelCase/lowercase field names used by most JSON APIs
        // even though property names are serialized as declared (PascalCase, unlike Newtonsoft's
        // opt-in [JsonProperty] naming, System.Text.Json ignores Newtonsoft attributes entirely).
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public T Deserialize<T>(Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, Options);
        }

        public Stream Serialize<T>(T data) where T : class
        {
            var json = JsonSerializer.Serialize(data, Options);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
