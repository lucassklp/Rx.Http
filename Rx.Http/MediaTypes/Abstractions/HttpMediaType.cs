using Rx.Http.Serializers.Body;
using System;
using System.Linq;

namespace Rx.Http.MediaTypes.Abstractions
{
    public abstract class HttpMediaType
    {
        public BodySerializer Body { get; private set; }

        public HttpMediaType(BodySerializer serializer)
        {
            Body = serializer;
        }

        public bool CanSerialize(string mediaType)
        {
            return !string.IsNullOrEmpty(mediaType) &&
                SupportedMimeTypes.Contains(mediaType.ToLower().Trim());
        }

        public abstract string[] SupportedMimeTypes { get; }
    }
}
