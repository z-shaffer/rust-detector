using Microsoft.EntityFrameworkCore;

namespace RustDetector.api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JobDataContext>();
        dbContext.Database.Migrate();
    }
    
}