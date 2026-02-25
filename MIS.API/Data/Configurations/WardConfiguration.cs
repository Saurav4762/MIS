using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace Mis.API.Models;

public class wardConfiguration : IEntityTypeConfiguration<Ward>
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
