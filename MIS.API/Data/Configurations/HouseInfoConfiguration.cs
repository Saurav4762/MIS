using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class HouseInfoConfiguration : IEntityTypeConfiguration<HouseInfo>

{
    public void Configure(EntityTypeBuilder<HouseInfo> entity)
    {
        entity.Property(h => h.Coords).HasColumnType("geography (point)");
        entity.HasOne(h => h.Tole)
            .WithMany(t => t.HouseInfos)
            .HasForeignKey(h => h.ToleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}