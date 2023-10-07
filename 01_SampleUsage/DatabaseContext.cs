using _01_SampleUsage.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_SampleUsage;

public class DatabaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }
}