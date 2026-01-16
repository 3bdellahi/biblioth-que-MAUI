using System.Text.Json.Serialization;

namespace project.Models;

public class Note
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("bookId")]
    public string BookId { get; set; }

  
    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime DateAdded { get; set; } = DateTime.Now;
}