using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public interface IJobDataRepository
{
    IEnumerable<JobData> GetAll();
    JobData? Get(int id);
    void Create(JobData jobData);
    void Update(JobData updatedJobData);
    void Delete(int id);
}