using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> entity)
    {
        entity.HasOne(w => w.Municipality)
            .WithMany(m => m.Wards)
            .HasForeignKey(w => w.MunicipalityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}