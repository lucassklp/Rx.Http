using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rx.Http.MediaTypes
{
    public class JsonHttpMediaType : HttpMediaType
    {

        public JsonHttpMediaType() : base(new JsonSerializer())
        {
        }

        public override string[] SupportedMimeTypes => new string[] { "application/json" };

    }
}
