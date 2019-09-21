using Rx.Http.MediaTypes.Abstractions;
using System.Collections.Generic;

namespace Rx.Http.MediaTypes
{
    public static class MediaTypesMap
    {
        private static readonly IHttpMediaType TextMediaType = new TextHttpMediaType();
        private static readonly IHttpMediaType JsonMediaType = new JsonHttpMediaType();

        public static Dictionary<string, IHttpMediaType> List() =>
            new Dictionary<string, IHttpMediaType>()
            {
                {MediaType.Application.Json, JsonMediaType},
                {MediaType.Text.Css, TextMediaType},
                {MediaType.Text.Csv, TextMediaType},
                {MediaType.Text.Html, TextMediaType},
                {MediaType.Text.Plain, TextMediaType},
                {MediaType.Text.Xml, TextMediaType}
            };

        public static IHttpMediaType Get(string mimeType) => List()[mimeType];

    }
}
