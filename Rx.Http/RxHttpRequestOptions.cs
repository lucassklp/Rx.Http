using Rx.Http.Serializers;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http
{
    public class RxHttpRequestOptions
    {
        public ISerializer Serializer {get;set;}
        public IDeserializer Deserializer {get;set;}
    }
}