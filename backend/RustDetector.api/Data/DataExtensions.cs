using Microsoft.EntityFrameworkCore;
using RustDetector.api.Repositories;

namespace RustDetector.api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JobDataContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("JobDataContext");
        services.AddSqlServer<JobDataContext>(connString)
            .AddScoped<IJobDataRepository, EntityFrameworkJobDataRepository>();
        
        return services;
    }
}