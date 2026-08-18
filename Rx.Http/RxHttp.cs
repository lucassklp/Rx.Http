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
            // Uses System.Text.Json by default; swap for NewtonsoftJsonSerializer() (or any other
            // ITwoWaysSerializable) here to change it library-wide.
            public static ITwoWaysSerializable Serializable { get; set; } = new NativeJsonSerializer();
            public static IHttpMediaTypeSerializer RequestMediaType { get; set; } = new JsonHttpMediaType(new NativeJsonSerializer());
            public static IHttpMediaTypeDeserializer ResponseMediaType { get; set; } = new JsonHttpMediaType(new NativeJsonSerializer());
        }
    }
}
