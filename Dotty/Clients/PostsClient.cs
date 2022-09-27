using Dotty.Models;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Net;

namespace Dotty.Clients;

public class PostsClient : IPostsClient
{

    public const string ClientName = "postsclient";

    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
         Policy<HttpResponseMessage>
             .Handle<HttpRequestException>()
             .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
             .WaitAndRetryAsync(
                 Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));
    private readonly IHttpClientFactory _httpClientFactory;

    public PostsClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PostsResponse?> GetPostsAsync()
    {
        var client = _httpClientFactory.CreateClient(ClientName);
        var response = await client.GetAsync("posts");

        return await response.Content.ReadFromJsonAsync<PostsResponse>();
    }
}

public interface IPostsClient
{
    Task<PostsResponse?> GetPostsAsync();
}