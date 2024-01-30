using Microsoft.EntityFrameworkCore;
using RustDetector.api.Repositories;

namespace RustDetector.api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        // Scope managing the lifetime of services
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JobDataContext>();
        // Apply any pending migrations
        await dbContext.Database.MigrateAsync();
        // Retrieve the repository. Ready to initialize
        var jobDataRepository = scope.ServiceProvider.GetRequiredService<IJobDataRepository>();
        jobDataRepository.InitializeAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Establish DB connection
        var connString = configuration.GetConnectionString("JobDataContext");
        services.AddSqlServer<JobDataContext>(connString)
            .AddScoped<IJobDataRepository, EntityFrameworkJobDataRepository>();
        // .NET cache builder
        services.AddMemoryCache();
        services.AddScoped<IJobDataRepository, EntityFrameworkJobDataRepository>();
        return services;
    }
}