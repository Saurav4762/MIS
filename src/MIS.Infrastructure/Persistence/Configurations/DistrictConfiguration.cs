using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace MIS.Infrastructure.Persistence.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasIndex(d => new { d.ProvinceId, d.Code })
            .IsUnique();

        builder.Property(d => d.Code)
            .IsRequired();

        builder.Property(d => d.NameEn)
            .IsRequired();

        builder.Property(d => d.NameNe)
            .IsRequired();

        builder.HasOne(d => d.Province)
            .WithMany(p => p.Districts)
            .HasForeignKey(d => d.ProvinceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
