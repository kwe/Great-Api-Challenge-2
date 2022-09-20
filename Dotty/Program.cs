var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/joke", () => new Joke
{
  Text = "What do you call a bear with no teeth? A gummy bear!",
  Category = "animal"
});

// return a joke as a json object
app.MapGet("/joke", () => new { joke = "What do you call a bear with no teeth? A gummy bear!" })
  .WithName("GetJoke")
  .WithOpenApi();

app.Run();

// define a Joke record
record Joke(string Text, string Category);