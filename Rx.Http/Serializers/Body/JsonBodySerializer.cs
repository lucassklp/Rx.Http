using Rx.Http.Serializers.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Rx.Http.Serializers.Body
{
    class JsonBodySerializer : BodySerializer
    {
        public JsonBodySerializer(ITwoWaysSerializable serializable): base(serializable)
        {
        }

        public override HttpContent Serialize(object obj)
        {
            var stream = this.serializer.Serialize(obj);
            return new StreamContent(stream);
        }
    }
}
