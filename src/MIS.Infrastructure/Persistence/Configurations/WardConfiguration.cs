using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace Mis.Infrastructure.Persistence.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Code).IsRequired();
        entity.HasIndex(e => new { e.MunicipalityId, e.Code }).IsUnique();

        entity.Property(e => e.Number);

        entity.Property(e => e.RepresentativeNameEn).IsRequired();
        entity.Property(e => e.RepresentativeNameNe).IsRequired();


        entity.HasOne(m => m.Municipality)
            .WithMany(m => m.Wards)
            .HasForeignKey(m => m.MunicipalityId);

    }

}
