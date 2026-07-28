using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class FacilitiesConfiguration : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        // Table Name
        builder.ToTable("Facilities");

        // Primary Key
        builder.HasKey(f => f.Id);

        // Foreign Key: Family (Required)
        builder.HasOne(f => f.Family)
            .WithMany()                          // Add collection in Family class if needed
            .HasForeignKey(f => f.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Facilities_Family");

        // Foreign Key: DrinkingWater
        builder.HasOne(f => f.DrinkingWater)
            .WithMany()
            .HasForeignKey(f => f.DrinkingWaterId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_DrinkingWater");

        // Foreign Key: ToiletType
        builder.HasOne(f => f.ToiletType)
            .WithMany()
            .HasForeignKey(f => f.ToiletTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_ToiletType");

        // Foreign Key: Electricity
        builder.HasOne(f => f.Electricity)
            .WithMany()
            .HasForeignKey(f => f.ElectricityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_Electricity");

        // Foreign Key: AltLight
        builder.HasOne(f => f.AltLight)
            .WithMany()
            .HasForeignKey(f => f.AltLightId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_AltLight");

        // Foreign Key: CookingFuel
        builder.HasOne(f => f.CookingFuel)
            .WithMany()
            .HasForeignKey(f => f.CookingFuelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_CookingFuel");

        // Foreign Key: StoveType
        builder.HasOne(f => f.StoveType)
            .WithMany()
            .HasForeignKey(f => f.StoveTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Facilities_StoveType");
    }
}