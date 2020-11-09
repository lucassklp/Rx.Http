using System.IO;

namespace Rx.Http.Serializers.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(Stream stream);
    }
}