using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using Rx.Http.Serializers.Body;
using System;
using System.Collections.Generic;
using System.Text;

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
