using Newtonsoft.Json;
using System;

namespace Models
{
    public class Todo : IEquatable<Todo>
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("completed")]
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