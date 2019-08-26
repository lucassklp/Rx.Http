using Rx.Http.Serializers.Interfaces;
using System.Net.Http;

namespace Rx.Http.Serializers.Body
{
    class JsonBodySerializer : BodySerializer
    {
        public JsonBodySerializer(ITwoWaysSerializable serializable) : base(serializable)
        {
        }

        public override HttpContent Serialize(object obj)
        {
            var stream = this.Serializer.Serialize(obj);
            return new StreamContent(stream);
        }
    }
}
