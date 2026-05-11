using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace MIS.Infrastructure.Persistence.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.Property(p => p.Code)
            .IsRequired();

        builder.Property(p => p.NameEn)
            .IsRequired();

        builder.Property(p => p.NameNe)
            .IsRequired();
    }
}
