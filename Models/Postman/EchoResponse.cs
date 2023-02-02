using Newtonsoft.Json;
using Rx.Http;
using System.Collections.Generic;

namespace Models.Postman
{
    public class EchoResponse
    {
        [JsonProperty("args")]
        public Dictionary<string, string> Args { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty("cookies")]
        public ListDictionary<string, string> Cookies { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("files")]
        public Dictionary<string, string> Files { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty("json")]
        public Dictionary<string, string> Json { get; set; }

    }
}
