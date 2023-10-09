using _03_Ef_Core_GlobalFiltering.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _03_Ef_Core_GlobalFiltering.Mappings;

public class CompanyMappings:IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies").HasKey(x=>x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

    }
}