namespace Rx.Http.Serializers.Interfaces
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T data) where T: class;
    }
    
//    public interface ISerializer
//    {
//        byte[] Serialize<T>(T data);
//    }
}