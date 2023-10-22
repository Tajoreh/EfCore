using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace _04_EfCore_Seed.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options):base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}