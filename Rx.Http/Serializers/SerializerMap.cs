using System.Collections.Generic;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Serializers
{
    public static class SerializerMap
    {
        
        private static IBodySerializer textSerializable = new TextSerializer();
        private static IBodySerializer jsonSerializable = new JsonSerializer();

        private static Dictionary<string, IBodySerializer> serializableDict =
            new Dictionary<string, IBodySerializer>()
            {
                {"application/json", jsonSerializable},
                {"text/css", textSerializable},
                {"text/csv", textSerializable},
                {"text/html", textSerializable},
                {"text/javascript", textSerializable},
                {"text/plain", textSerializable},
                {"text/xml", textSerializable}
            };


        public static IBodySerializer GetSerializable(string mimeType) => serializableDict[mimeType];
    }
}