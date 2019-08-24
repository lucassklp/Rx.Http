using System.IO;

namespace Rx.Http.Serializers.Interfaces
{
    public interface ISerializer
    {
        Stream Serialize<T>(T data) where T: class;
    }
}