using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class HouseOwnerConfiguration : IEntityTypeConfiguration<HouseOwner>
{
    public void Configure(EntityTypeBuilder<HouseOwner> builder)
    {
        builder.ToTable("HouseOwners");
        builder.HasKey(ho => ho.HouseId);

        builder.Property(ho => ho.Other).HasMaxLength(500);

        builder.HasOne(ho => ho.House)
            .WithOne(h => h.HouseOwner)
            .HasForeignKey<HouseOwner>(ho => ho.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Person>(ho => ho.Person)
            .WithMany(p => p.HouseOwners)
            .HasForeignKey(ho => ho.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Institute>(ho => ho.Institute)
            .WithMany(i => i.HouseOwners)
            .HasForeignKey(ho => ho.InstituteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
