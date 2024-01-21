using System.Reflection;
using RustDetector.api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RustDetector.api.Data.Configurations;

public class JobDataConfiguration : IEntityTypeConfiguration<JobData>
{
    public void Configure(EntityTypeBuilder<JobData> builder)
    {
        builder.Property(jobData => jobData.Year)
            .HasPrecision(4);
        builder.Property(jobData => jobData.Month)
            .HasPrecision(2);
    }
}