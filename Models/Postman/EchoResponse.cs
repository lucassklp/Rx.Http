using Rx.Http;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models.Postman
{
    public class EchoResponse
    {
        [JsonPropertyName("args")]
        public Dictionary<string, string> Args { get; set; }

        [JsonPropertyName("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonPropertyName("cookies")]
        public ListDictionary<string, string> Cookies { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("files")]
        public Dictionary<string, string> Files { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        [JsonPropertyName("json")]
        public Dictionary<string, string> Json { get; set; }

    }
}
