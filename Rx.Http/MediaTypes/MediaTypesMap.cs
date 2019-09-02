using Rx.Http.MediaTypes.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Rx.Http.MediaTypes
{
    public static class MediaTypesMap
    {

        public static IHttpMediaType TextMediaType = new TextHttpMediaType();
        private static IHttpMediaType JsonMediaType = new JsonHttpMediaType();

        private static Dictionary<string, IHttpMediaType> mediaTypes =
            new Dictionary<string, IHttpMediaType>()
            {
                {MediaType.Application.Json, JsonMediaType},
                {MediaType.Text.Css, TextMediaType},
                {MediaType.Text.Csv, TextMediaType},
                {MediaType.Text.Html, TextMediaType},
                {MediaType.Text.Javascript, TextMediaType},
                {MediaType.Text.Plain, TextMediaType},
                {MediaType.Text.Xml, TextMediaType}
            };


        public static IHttpMediaType GetMediaType(string mimeType) => mediaTypes[mimeType];

        public static string[] GetAllMimeTypes() => mediaTypes.Keys.ToArray();
    }
}
