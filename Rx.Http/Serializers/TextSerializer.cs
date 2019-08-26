using Rx.Http.Serializers.Interfaces;
using System.IO;
using System.Text;

namespace Rx.Http.Serializers
{
    public class TextSerializer : ITwoWaysSerializable
    {
        public T Deserialize<T>(Stream stream) where T : class
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd() as T;
            }
        }

        public Stream Serialize<T>(T data)
            where T : class
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(data.ToString()));
        }
    }
}