using RustDetector.api.Entities;
using RustDetector.api.Repositories;

namespace RustDetector.api.Endpoints;

public static class JobDataEndpoints
{
    const string JobDataEndpointName = "GetJobData";
    
    public static RouteGroupBuilder MapJobDataEndpoints(this IEndpointRouteBuilder routes)
    {
        JobDataRepository repository = new JobDataRepository();
        
        var group = routes.MapGroup("/jobdata")
            .WithParameterValidation();

        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            JobData? jobData = repository.Get(id);
            return jobData is not null ? Results.Ok(jobData) : Results.NotFound();
        }).WithName(JobDataEndpointName);

        group.MapPost("/", (JobData jobData) =>
        {
            repository.Create(jobData);

            return Results.CreatedAtRoute(JobDataEndpointName, new {id = jobData.Id}, jobData);
        });

        group.MapPut("/{id}", (int id, JobData updatedJobData) =>
        {
            JobData? existingJobData = repository.Get(id);

            if (existingJobData is null)
            {
                return Results.NotFound();
            }

            existingJobData.RustCount = updatedJobData.RustCount;
            existingJobData.GoCount = updatedJobData.GoCount;
            existingJobData.PythonCount = updatedJobData.PythonCount;
            repository.Update(existingJobData);
            
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            JobData? jobData = repository.Get(id);

            if (jobData is not null)
            {
                repository.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}