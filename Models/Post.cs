using Newtonsoft.Json;

namespace Models
{
    public class Post
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonProperty("userId")]
        public int UserId { get; set; }

    }
}
