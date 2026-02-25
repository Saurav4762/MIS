using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        builder.HasKey(w => w.Id);
        builder.HasIndex(w => new { w.MunicipalityId, w.Code }).IsUnique();
        builder.Property(w => w.Code).IsRequired();
        builder.Property(w => w.Name).IsRequired();

        builder.HasOne(W => W.Municipality)
            .WithMany(m => m.Wards)
            .HasForeignKey(W => W.MunicipalityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
