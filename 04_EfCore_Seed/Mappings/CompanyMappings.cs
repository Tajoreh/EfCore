using _04_EfCore_Seed.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _04_EfCore_Seed.Mappings;

public class CompanyMappings:IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies").HasKey(x=>x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        //Applies to all queries
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(

            new Company()
            {
                Id = 1,
                IsDeleted = false,
                Name = "Some1",
                LastSalaryUpdateUtc = DateTime.UtcNow
            },
            new Company()
            {
                Id = 2,
                IsDeleted = false,
                Name = "Some2",
                LastSalaryUpdateUtc = DateTime.UtcNow
            }
        );

    }
}