using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public interface IJobDataRepository
{
    Task<IEnumerable<JobData>> GetAllAsync();
    Task<JobData?> GetAsync(int id);
    Task CreateAsync(JobData jobData);
    Task UpdateAsync(JobData updatedJobData);
    Task DeleteAsync(int id);
    Task InitializeAsync();
}