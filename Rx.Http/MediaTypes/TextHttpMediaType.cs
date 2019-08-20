using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;

namespace Rx.Http.MediaTypes
{
    public class TextHttpMediaType : HttpMediaType
    {
        public TextHttpMediaType() : base(new TextSerializer())
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