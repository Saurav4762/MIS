using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace MIS.Infrastructure.Persistence.Configurations;

public class Municipalityconfiguration : IEntityTypeConfiguration<Municipality>

{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder
            .HasIndex(m => m.NameNe)
            .IsUnique();
        builder
            .HasIndex(m => m.NameEn)
            .IsUnique();

        builder
            .HasIndex(m => m.Code)
            .IsUnique();

    }
}