using System.Text.Json.Serialization;

namespace Models
{
    public class Post
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

    }
}
