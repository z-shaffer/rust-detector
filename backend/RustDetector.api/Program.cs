using RustDetector.api.Data;
using RustDetector.api.Endpoints;
using RustDetector.api.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IJobDataRepository, JobDataRepository>();

var connString = builder.Configuration.GetConnectionString("JobDataContext");
builder.Services.AddSqlServer<JobDataContext>(connString);

var app = builder.Build();

app.Services.InitializeDb();

app.MapJobDataEndpoints();

app.Run();
