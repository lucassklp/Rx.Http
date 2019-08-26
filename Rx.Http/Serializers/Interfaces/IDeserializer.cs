using System.IO;

namespace Rx.Http.Serializers.Interfaces
{
    //    public interface IDeserializer<out T>
    //        where T: class
    //    {
    //        T Deserialize(Stream stream);
    //    }
    //    
    public interface IDeserializer
    {
        T Deserialize<T>(Stream stream) where T : class;
    }
}