using Newtonsoft.Json;
using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Text;

namespace Rx.Http.Serializers
{
    public class JsonSerializer : ITwoWaysSerializable
    {
        public T Deserialize<T>(Stream stream) where T : class
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(sr))
                {
                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public Stream Serialize<T>(T data)
            where T : class
        {
            var content = JsonConvert.SerializeObject(data);
            byte[] byteArray = Encoding.ASCII.GetBytes(content);
            return new MemoryStream(byteArray);
        }
    }
}