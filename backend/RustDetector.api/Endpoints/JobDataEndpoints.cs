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

        group.MapGet("/", (IJobDataRepository repository) => 
            repository.GetAll().Select(jobData => jobData.AsDto()));

        group.MapGet("/{id}", (IJobDataRepository repository, int id) =>
        {
            JobData? jobData = repository.Get(id);
            return jobData is not null ? Results.Ok(jobData.AsDto()) : Results.NotFound();
        }).WithName(JobDataEndpointName);

        group.MapPost("/", (IJobDataRepository repository, CreateJobDataDto jobDataDto) =>
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
            repository.Create(jobData);

            return Results.CreatedAtRoute(JobDataEndpointName, new {id = jobData.Id}, jobData);
        });

        group.MapPut("/{id}", (IJobDataRepository repository, int id, UpdateJobDataDto updatedJobDataDto) =>
        {
            JobData? existingJobData = repository.Get(id);

            if (existingJobData is null)
            {
                return Results.NotFound();
            }

            existingJobData.RustCount = updatedJobDataDto.RustCount;
            existingJobData.GoCount = updatedJobDataDto.GoCount;
            existingJobData.PythonCount = updatedJobDataDto.PythonCount;
            repository.Update(existingJobData);
            
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IJobDataRepository repository, int id) =>
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