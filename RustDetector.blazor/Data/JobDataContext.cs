using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RustDetector.api.Entities;

namespace RustDetector.api.Data;

public class JobDataContext : DbContext
{
    // Future dbcontext options get configured here
    public JobDataContext(DbContextOptions<JobDataContext> options)
        : base(options)
    {
        
    }
    public DbSet<JobData> JobDataSet => Set<JobData>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations for the dbcontext model
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}