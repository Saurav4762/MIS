using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class ToleConfiguration : IEntityTypeConfiguration<Tole>
{
    public void Configure(EntityTypeBuilder<Tole> entity)
    {
        entity.HasOne(t => t.Area)
            .WithMany(a => a.Toles)
            .HasForeignKey(t => t.AreaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}