using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class HouseConfiguration : IEntityTypeConfiguration<House>

{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.HasOne(t => t.Tole)
            .WithMany(m => m.Houses)
            .HasForeignKey(t => t.ToleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}