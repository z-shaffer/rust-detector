using RustDetector.api.Entities;

namespace RustDetector.api.Endpoints;

public static class JobDataEndpoints
{
    const string JobDataEndpointName = "GetJobData";

    static List<JobData> _jobDataList = new()
    {
        new JobData()
        {
            Id = 1,
            Month = 2,
            Year = 2024,
            RustCount = 1000,
            GoCount = 2000,
            PythonCount = 3000
        },
        new JobData()
        {
            Id = 2,
            Month = 3,
            Year = 2025,
            RustCount = 100,
            GoCount = 200,
            PythonCount = 300
        }
    };
    
    public static RouteGroupBuilder MapJobDataEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/jobdata")
            .WithParameterValidation();

        group.MapGet("/", () => _jobDataList);

        group.MapGet("/{id}", (int id) =>
        {
            JobData? jobData = _jobDataList.Find(jobData => jobData.Id == id);

            if (jobData is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(jobData);
        }).WithName(JobDataEndpointName);

        group.MapPost("/", (JobData jobData) =>
        {
            jobData.Id = _jobDataList.Max(data => data.Id) + 1;
            _jobDataList.Add(jobData);

            return Results.CreatedAtRoute(JobDataEndpointName, new {id = jobData.Id}, jobData);
        });

        group.MapPut("/{id}", (int id, JobData updatedJobData) =>
        {
            JobData? existingJobData = _jobDataList.Find(jobData => jobData.Id == id);

            if (existingJobData is null)
            {
                return Results.NotFound();
            }

            existingJobData.RustCount = updatedJobData.RustCount;
            existingJobData.GoCount = updatedJobData.GoCount;
            existingJobData.PythonCount = updatedJobData.PythonCount;

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            JobData? jobData = _jobDataList.Find(jobData => jobData.Id == id);

            if (jobData is not null)
            {
                _jobDataList.Remove(jobData);
            }

            return Results.NoContent();
        });

        return group;
    }
}