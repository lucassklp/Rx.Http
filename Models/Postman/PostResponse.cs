using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models.Postman
{
    public class PostResponse : PostResponse<string>
    {

    }

    public class PostResponse<T>
    {
        [JsonPropertyName("args")]
        public Dictionary<string, string> Args { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("form")]
        public Dictionary<string, string> Form { get; set; }

        [JsonPropertyName("headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}
