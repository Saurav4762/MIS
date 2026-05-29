using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.House_info;

namespace MIS.Infrastructure.Persistence.Configurations;

public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        // Primary Key
        builder.HasKey(h => h.Id);

        // Required Properties
        builder.Property(h => h.HouseNumber).IsRequired();
        builder.Property(h => h.Location).IsRequired();
        
        // Indexes for performance
        builder.HasIndex(h => new { h.WardId, h.ToleId, h.HouseNumber }).IsUnique();
        
        // Foreign Key: Ward
        builder.HasOne(h => h.Ward)
            .WithMany()
            .HasForeignKey(h => h.WardId)
            .OnDelete(DeleteBehavior.Cascade);
        // Foreign Key: Tole
        builder.HasOne(h => h.Tole)
            .WithMany()
            .HasForeignKey(h => h.ToleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Foreign Keys for House Characteristics (OptionItems)
        builder.HasOne(h => h.HouseType)
            .WithMany()
            .HasForeignKey(h => h.HouseTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_House_HouseType");

        builder.HasOne(h => h.LandType)
            .WithMany()
            .HasForeignKey(h => h.LandTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_House_LandType");

        builder.HasOne(h => h.RoofType)
            .WithMany()
            .HasForeignKey(h => h.RoofTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_House_RoofType");

        builder.HasOne(h => h.WallType)
            .WithMany()
            .HasForeignKey(h => h.WallTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_House_WallType");
    }
}
