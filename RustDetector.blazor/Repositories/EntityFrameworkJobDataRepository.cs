using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RustDetector.api.Data;
using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class EntityFrameworkJobDataRepository(JobDataContext context, IMemoryCache cache) : IJobDataRepository
{
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromHours(24);
    private const string DataKey = "JobDataCache";

    // Fetches data for GET requests
    public async Task<IEnumerable<JobData>> GetAllAsync()
    {
        // Check if the cache contains the job data
        // The cache generally should always contain up to date data for fast loading
        // This is a safety net
        if (!cache.TryGetValue(DataKey, out IEnumerable<JobData> cachedData))
        {
            // Fill the cache with up to date data from db
            cachedData = await context.JobDataSet.AsNoTracking().ToListAsync();
            cache.Set(DataKey, cachedData, _cacheExpiration);
        }
        // Data is now in cache, return it
        return cachedData;
    }

    // For manual API GET requestss, so does not need a cache
    public async Task<JobData?> GetAsync(int id)
    {
        return await context.JobDataSet.FindAsync(id);
    }

    // Manual API POST requests
    public async Task CreateAsync(JobData jobData)
    {
        context.JobDataSet.Add(jobData);
        await context.SaveChangesAsync();
    }

    // Manual API PUT requests
    public async Task UpdateAsync(JobData updatedJobData)
    {
        context.Update(updatedJobData);
        await context.SaveChangesAsync();
    }

    // Manual API DELETE requests
    public async Task DeleteAsync(int id)
    {
        await context.JobDataSet.Where(jobData => jobData.Id == id)
            .ExecuteDeleteAsync();
    }
    
    // On entity init
   public async Task InitializeAsync()
   {
       // Small delay while db connection establishes 
       TimeSpan initDelay = TimeSpan.FromSeconds(5);
       await Task.Delay(initDelay);
       // Immediately fill cache with up to date data
       IEnumerable<JobData> cachedData = await context.JobDataSet.AsNoTracking().ToListAsync();
       cache.Set(DataKey, cachedData, _cacheExpiration);
       // Check for new data every 24 hours and fill the cache
       while (true)
       {
           await Task.Delay(_cacheExpiration);
           cachedData = await context.JobDataSet.AsNoTracking().ToListAsync();
           cache.Set(DataKey, cachedData, _cacheExpiration);
       }
   }
}