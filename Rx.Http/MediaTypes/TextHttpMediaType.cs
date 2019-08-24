using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Body;

namespace Rx.Http.MediaTypes
{
    public class TextHttpMediaType : HttpMediaType
    {
        public TextHttpMediaType() : base(new TextBodySerializer())
        {
        }

        public override string[] SupportedMimeTypes => new string[]
        {
            "text/css",
            "text/csv",
            "text/html",
            "text/javascript",
            "text/plain",
            "text/xml"
        };
    }
}