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
        // Query all db entries
        group.MapGet("/", async (IJobDataRepository repository) => 
            (await repository.GetAllAsync()).Select(jobData => jobData.AsDto()));

        // Query specific entry based on id
        group.MapGet("/{id}", async (IJobDataRepository repository, int id) =>
        {
            var jobData = await repository.GetAsync(id);
            return jobData is not null ? Results.Ok(jobData.AsDto()) : Results.NotFound();
            // Will be used on completion of a POST request
        }).WithName(JobDataEndpointName);

        // Manually add a db entry
        group.MapPost("/", async (IJobDataRepository repository, CreateJobDataDto jobDataDto) =>
        {
            JobData jobData = new()
            {
                Id = jobDataDto.Id,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                RustCount = jobDataDto.RustCount,
                GoCount = jobDataDto.GoCount,
                PythonCount = jobDataDto.PythonCount
            };
            await repository.CreateAsync(jobData);
            // Use GET request with id to display results
            return Results.CreatedAtRoute(JobDataEndpointName, new {id = jobData.Id}, jobData);
        });

        // Manually update a db entry
        group.MapPut("/{id}", async (IJobDataRepository repository, int id, UpdateJobDataDto updatedJobDataDto) =>
        {
            var existingJobData = await repository.GetAsync(id);
            // Invalid id provided
            if (existingJobData is null)
            {
                return Results.NotFound();
            }
            // Valid id, update data
            existingJobData.RustCount = updatedJobDataDto.RustCount;
            existingJobData.GoCount = updatedJobDataDto.GoCount;
            existingJobData.PythonCount = updatedJobDataDto.PythonCount;
            await repository.UpdateAsync(existingJobData);
            
            return Results.NoContent();
        });

        // Manually delete a DB entry
        group.MapDelete("/{id}", async (IJobDataRepository repository, int id) =>
        {
            var jobData = await repository.GetAsync(id);
            // Invalid id provided
            if (jobData is not null)
            {
                await repository.DeleteAsync(id);
            }
            // Valid id provided, delete entry
            return Results.NoContent();
        });

        return group;
    }
}