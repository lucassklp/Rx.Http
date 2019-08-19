using System.IO;
using System.Text;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public class TextSerializer : IBodySerializer
    {
        public T Deserialize<T>(Stream stream) where T: class
        {
            using(var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd() as T;
            }
        }

        public byte[] Serialize<T>(T data) where T: class
        {
            return Encoding.ASCII.GetBytes(data.ToString());
        }
        
    }
}