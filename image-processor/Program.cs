using ImageProcessor.Endpoints;

const string CorsString = "corsString";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(IEndpoint));

builder.Services.AddCors(options => {
  options.AddPolicy(name: CorsString, builder => {
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
  });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CorsString);

app.UseEndpoints();

app.Run();

public partial class Program { }