﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RustDetector.api.Data;

#nullable disable

namespace RustDetector.api.DataMigrations
{
    [DbContext(typeof(JobDataContext))]
    partial class JobDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RustDetector.api.Entities.JobData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GoCount")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasPrecision(2)
                        .HasColumnType("int");

                    b.Property<int>("PythonCount")
                        .HasColumnType("int");

                    b.Property<int>("RustCount")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasPrecision(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JobDataSet");
                });
#pragma warning restore 612, 618
        }
    }
}