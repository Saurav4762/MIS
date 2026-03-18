using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace Mis.Infrastructure.Persistence.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired();

        entity.HasOne(m => m.Municipality)
            .WithMany(m => m.Wards)
            .HasForeignKey(m => m.MunicipalityId);

    }

} 
