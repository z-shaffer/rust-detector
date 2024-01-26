using RustDetector.api.Data;
using RustDetector.api.Endpoints;
using RustDetector.api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeDbAsync();

//app.UseMiddleware<RestrictToLocalhostMiddleware>();

app.MapJobDataEndpoints();

app.Run();
