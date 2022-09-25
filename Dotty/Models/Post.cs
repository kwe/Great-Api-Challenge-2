using System.Text.Json.Serialization;

namespace Dotty.Models;

public class Post
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }
}

public class PostsResponse
{
    public Post[]? Post { get; set; }
}