using Polly;
using Polly.Contrib.WaitAndRetry;
using Dotty.Clients;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPostsClient, PostsClient>();

builder.Services.AddHttpClient(PostsClient.ClientName,
    client =>
    {
      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    })
// .AddTransientHttpErrorPolicy(policyBuilder =>
// policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));
.AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
    .WaitAndRetryAsync(
        Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// // return a joke as a json object
// app.MapGet("/joke", () => new { joke = "What do you call a bear with no teeth? A gummy bear!" })
//   .WithName("GetJoke")
//   .WithOpenApi();

app.Run();