using Newtonsoft.Json;
using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Text;

namespace Rx.Http.Serializers
{
    public class NewtonsoftJsonSerializer : ITwoWaysSerializable
    {
        public T Deserialize<T>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public Stream Serialize<T>(T data)
            where T : class
        {
            var content = JsonConvert.SerializeObject(data);
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            return new MemoryStream(byteArray);
        }
    }
}