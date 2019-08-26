using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Body;

namespace Rx.Http.MediaTypes
{
    public class JsonHttpMediaType : HttpMediaType
    {

        public JsonHttpMediaType() : base(new JsonBodySerializer(new JsonSerializer()))
        {
        }

        public override string[] SupportedMimeTypes => new string[] { "application/json" };

    }
}
