using Rx.Http.MediaTypes.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Rx.Http.MediaTypes
{
    public static class MediaTypesMap
    {

        public static HttpMediaType TextMediaType = new TextHttpMediaType();
        private static HttpMediaType JsonMediaType = new JsonHttpMediaType();

        private static Dictionary<string, HttpMediaType> mediaTypes =
            new Dictionary<string, HttpMediaType>()
            {
                {"application/json", JsonMediaType},
                {"text/css", TextMediaType},
                {"text/csv", TextMediaType},
                {"text/html", TextMediaType},
                {"text/javascript", TextMediaType},
                {"text/plain", TextMediaType},
                {"text/xml", TextMediaType}
            };


        public static HttpMediaType GetMediaType(string mimeType) => mediaTypes[mimeType];

        public static string[] GetAllMimeTypes() => mediaTypes.Keys.ToArray();
    }
}
