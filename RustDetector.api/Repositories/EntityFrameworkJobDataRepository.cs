using Microsoft.EntityFrameworkCore;
using RustDetector.api.Data;
using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class EntityFrameworkJobDataRepository(JobDataContext dbContext) : IJobDataRepository
{
    public async Task<IEnumerable<JobData>> GetAllAsync()
    {
        return await dbContext.JobDataSet.AsNoTracking().ToListAsync();
    }

    public async Task<JobData?> GetAsync(int id)
    {
        return await dbContext.JobDataSet.FindAsync(id);
    }

    public async Task CreateAsync(JobData jobData)
    {
        dbContext.JobDataSet.Add(jobData);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobData updatedJobData)
    {
        dbContext.Update(updatedJobData);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.JobDataSet.Where(jobData => jobData.Id == id)
            .ExecuteDeleteAsync();
    }
}