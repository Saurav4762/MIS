using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> entity)
    {
        entity.HasOne(a => a.Ward)
            .WithMany(w => w.Areas)
            .HasForeignKey(a => a.WardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}