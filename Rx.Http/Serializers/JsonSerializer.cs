using System.IO;
using System.Net.Http;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public class JsonSerializer : IBodySerializer
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

        public HttpContent CreateHttpContent(Stream stream)
        {
            return new StreamContent(stream);
        }

        public byte[] Serialize<T>(T data) where T: class
        {
            throw new System.NotImplementedException();
        }
        
    }
}