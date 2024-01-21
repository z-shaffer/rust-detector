using Microsoft.EntityFrameworkCore;
using RustDetector.api.Data;
using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class EntityFrameworkJobDataRepository(JobDataContext dbContext) : IJobDataRepository
{
    public IEnumerable<JobData> GetAll()
    {
        return dbContext.JobDataSet.AsNoTracking().ToList();
    }

    public JobData? Get(int id)
    {
        return dbContext.JobDataSet.Find(id);
    }

    public void Create(JobData jobData)
    {
        dbContext.JobDataSet.Add(jobData);
        dbContext.SaveChanges();
    }

    public void Update(JobData updatedJobData)
    {
        dbContext.Update(updatedJobData);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.JobDataSet.Where(jobData => jobData.Id == id)
            .ExecuteDelete();
    }
}