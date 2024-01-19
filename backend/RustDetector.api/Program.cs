using RustDetector.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapJobDataEndpoints();

app.Run();
