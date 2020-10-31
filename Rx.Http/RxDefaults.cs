using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;

namespace Rx.Http
{
    public static class RxDefaults
    {
        public static IHttpMediaTypeSerializer DefaultRequestMediaType { get; set; } = new JsonHttpMediaType(new NewtonsoftJsonSerializer());
        public static IHttpMediaTypeDeserializer DefaultResponseMediaType { get; set; } = new JsonHttpMediaType(new NewtonsoftJsonSerializer());
    }
}
