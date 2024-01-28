using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RustDetector.api.Data;
using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class EntityFrameworkJobDataRepository(JobDataContext dbContext) : IJobDataRepository
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheExpiration = GetDaysUntilEndOfMonth();
    private readonly string DataKey = "JobDataCache";
    
    public async Task<IEnumerable<JobData>> GetAllAsync()
    {
        if (!_cache.TryGetValue(DataKey, out IEnumerable<JobData> cachedData))
        {
            cachedData = await dbContext.JobDataSet.AsNoTracking().ToListAsync();
            _cache.Set(DataKey, cachedData, DateTime.Now.Add(_cacheExpiration));
        }

        return cachedData;
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
    
    private static TimeSpan GetDaysUntilEndOfMonth()
    {
        DateTime currentDate = DateTime.Now;
        DateTime firstDayOfNextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);
        DateTime lastDayOfCurrentMonth = firstDayOfNextMonth.AddDays(-1);
        TimeSpan daysUntilEndOfMonth = lastDayOfCurrentMonth - currentDate;

        return daysUntilEndOfMonth;
    }
}