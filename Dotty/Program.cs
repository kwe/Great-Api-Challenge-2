
using Polly;
using Polly.Contrib.WaitAndRetry;
using Dotty.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IPostsClient, PostsClient>();

builder.Services.AddHttpClient(PostsClient.ClientName,
    client =>
    {
      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

var app = builder.Build();

//app.UseHttpsRedirection();
// app.MapControllers();
// // return a joke as a json object
// app.MapGet("/joke", () => new { joke = "What do you call a bear with no teeth? A gummy bear!" })
//   .WithName("GetJoke")
//   .WithOpenApi();

app.MapGet("/posts", async (IPostsClient postsClient) =>
{
  var posts = await postsClient.GetPostsAsync();
  return posts is not null ? Results.Ok(posts) : Results.NotFound();
});

app.Run();