using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Todo : IEquatable<Todo>
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("completed")]
        public bool IsCompleted { get; set; }

        public bool Equals(Todo other)
        {
            return this.Id == other.Id &&
                this.IsCompleted == other.IsCompleted &&
                this.Title == other.Title &&
                this.UserId == other.UserId;
        }
    }
}