using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace _03_Ef_Core_GlobalFiltering.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? LastSalaryUpdateUtc { get; set; }
    public bool IsDeleted { get; set; }
}


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options):base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       base.OnModelCreating(modelBuilder);
    }
}