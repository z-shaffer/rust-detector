using Microsoft.EntityFrameworkCore;
using RustDetector.api.Repositories;

namespace RustDetector.api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JobDataContext>();
        await dbContext.Database.MigrateAsync();
        var jobDataRepository = scope.ServiceProvider.GetRequiredService<IJobDataRepository>();
        jobDataRepository.InitializeAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("JobDataContext");
        services.AddSqlServer<JobDataContext>(connString)
            .AddScoped<IJobDataRepository, EntityFrameworkJobDataRepository>();
        services.AddMemoryCache();
        services.AddScoped<IJobDataRepository, EntityFrameworkJobDataRepository>();
        return services;
    }
}