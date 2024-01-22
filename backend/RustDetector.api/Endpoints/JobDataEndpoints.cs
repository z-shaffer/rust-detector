using RustDetector.api.Entities;
using RustDetector.api.Repositories;

namespace RustDetector.api.Endpoints;

public static class JobDataEndpoints
{
    const string JobDataEndpointName = "GetJobData";
    
    public static RouteGroupBuilder MapJobDataEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/jobdata")
            .WithParameterValidation();

        group.MapGet("/", async (IJobDataRepository repository) => 
            (await repository.GetAllAsync()).Select(jobData => jobData.AsDto()));

        group.MapGet("/{id}", async (IJobDataRepository repository, int id) =>
        {
            JobData? jobData = await repository.GetAsync(id);
            return jobData is not null ? Results.Ok(jobData.AsDto()) : Results.NotFound();
        }).WithName(JobDataEndpointName);

        group.MapPost("/", async (IJobDataRepository repository, CreateJobDataDto jobDataDto) =>
        {
            JobData jobData = new()
            {
                Id = jobDataDto.Id,
                Month = jobDataDto.Month,
                Year = jobDataDto.Year,
                RustCount = jobDataDto.RustCount,
                GoCount = jobDataDto.GoCount,
                PythonCount = jobDataDto.PythonCount
            };
            await repository.CreateAsync(jobData);

            return Results.CreatedAtRoute(JobDataEndpointName, new {id = jobData.Id}, jobData);
        });

        group.MapPut("/{id}", async (IJobDataRepository repository, int id, UpdateJobDataDto updatedJobDataDto) =>
        {
            JobData? existingJobData = await repository.GetAsync(id);

            if (existingJobData is null)
            {
                return Results.NotFound();
            }

            existingJobData.RustCount = updatedJobDataDto.RustCount;
            existingJobData.GoCount = updatedJobDataDto.GoCount;
            existingJobData.PythonCount = updatedJobDataDto.PythonCount;
            await repository.UpdateAsync(existingJobData);
            
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (IJobDataRepository repository, int id) =>
        {
            JobData? jobData = await repository.GetAsync(id);

            if (jobData is not null)
            {
                repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}