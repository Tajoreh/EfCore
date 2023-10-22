﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _04_EfCore_Seed.Entities;

#nullable disable

namespace _04_EfCore_Seed.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_04_EfCore_Seed.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastSalaryUpdateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            LastSalaryUpdateUtc = new DateTime(2023, 10, 21, 8, 54, 14, 208, DateTimeKind.Utc).AddTicks(9177),
                            Name = "Some1"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            LastSalaryUpdateUtc = new DateTime(2023, 10, 21, 8, 54, 14, 208, DateTimeKind.Utc).AddTicks(9181),
                            Name = "Some2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
