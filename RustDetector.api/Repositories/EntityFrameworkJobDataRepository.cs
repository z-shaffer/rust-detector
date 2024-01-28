using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RustDetector.api.Data;
using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class EntityFrameworkJobDataRepository(JobDataContext context, IMemoryCache cache) : IJobDataRepository
{
    private readonly TimeSpan _cacheExpiration = GetDaysUntilEndOfMonth();
    private const string DataKey = "JobDataCache";

    public async Task<IEnumerable<JobData>> GetAllAsync()
    {
        if (!cache.TryGetValue(DataKey, out IEnumerable<JobData> cachedData))
        {
            cachedData = await context.JobDataSet.AsNoTracking().ToListAsync();
            cache.Set(DataKey, cachedData, DateTime.Now.Add(_cacheExpiration));
        }

        return cachedData;
    }

    public async Task<JobData?> GetAsync(int id)
    {
        return await context.JobDataSet.FindAsync(id);
    }

    public async Task CreateAsync(JobData jobData)
    {
        context.JobDataSet.Add(jobData);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobData updatedJobData)
    {
        context.Update(updatedJobData);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await context.JobDataSet.Where(jobData => jobData.Id == id)
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