using RustDetector.api.Endpoints;
using RustDetector.api.Entities;
using RustDetector.api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IJobDataRepository, JobDataRepository>();

var connString = builder.Configuration.GetConnectionString("JobDataContext");

var app = builder.Build();

app.MapJobDataEndpoints();

app.Run();
