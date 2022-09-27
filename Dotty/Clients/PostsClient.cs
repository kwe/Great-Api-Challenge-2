using Dotty.Models;

namespace Dotty.Clients;

public class PostsClient : IPostsClient
{

  public const string ClientName = "postsclient";
  
  private readonly IHttpClientFactory _httpClientFactory;

  public PostsClient(IHttpClientFactory httpClientFactory)
  {
    _httpClientFactory = httpClientFactory;
  }

  public async Task<IEnumerable<Post>?> GetPostsAsync()
  {
    var client = _httpClientFactory.CreateClient(ClientName);
    var response = await client.GetAsync("posts");

    return await response.Content.ReadFromJsonAsync<IEnumerable<Post>>();
  }
}

public interface IPostsClient
{
  Task<IEnumerable<Post>?> GetPostsAsync();
}