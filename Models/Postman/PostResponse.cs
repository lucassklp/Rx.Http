using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Postman
{
    public class PostResponse : PostResponse<string>
    {

    }

    public class PostResponse<T>
    {
        [JsonProperty("args")]
        public Dictionary<string, string> Args { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("form")]
        public Dictionary<string, string> Form { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}
