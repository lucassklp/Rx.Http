using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Rx.Http.Serializers.Body
{
    class TextBodySerializer : BodySerializer
    {
        public TextBodySerializer() : base(new TextSerializer())
        {
        }

        public override HttpContent Serialize(object obj)
        {
            return new StreamContent(this.Serializer.Serialize(obj));
        }
    }
}
