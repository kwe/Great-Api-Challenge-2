
using Polly;
using Polly.Contrib.WaitAndRetry;
using Dotty.Clients;
using Dotty.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPostsClient, PostsClient>();

builder.Services.AddHttpClient(PostsClient.ClientName,
    client =>
    {
      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

var app = builder.Build();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => new { Message = "Hello World!" })
    .WithName("HelloWorld");

app.MapGet("/time", () => new { Time = DateTime.Now })
    .WithName("GetTime");

app.MapGet("/joke", () => new { joke = "What do you call a bear with no teeth? A gummy bear!" })
    .WithName("GetJoke");

app.MapGet("/posts", async (IPostsClient postsClient) =>
    {
        var posts = await postsClient.GetPostsAsync();
        return posts is not null ? Results.Ok(posts) : Results.NotFound();
    })
    .WithName("GetPosts")
    .Produces<IEnumerable<Post>>();
app.Run();