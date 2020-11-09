using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http
{
    public static class RxHttp
    {
        public static class Default
        {
            public static ITwoWaysSerializable Serializable { get; set; } = new NewtonsoftJsonSerializer();
            public static IHttpMediaTypeSerializer RequestMediaType { get; set; } = new JsonHttpMediaType(new NewtonsoftJsonSerializer());
            public static IHttpMediaTypeDeserializer ResponseMediaType { get; set; } = new JsonHttpMediaType(new NewtonsoftJsonSerializer());
        }
    }
}
