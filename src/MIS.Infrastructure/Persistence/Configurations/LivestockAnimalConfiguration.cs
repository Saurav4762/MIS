using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class LivestockAnimalConfiguration : IEntityTypeConfiguration<LivestockAnimal>
{
    public void Configure(EntityTypeBuilder<LivestockAnimal> builder)
    {
        builder.ToTable("LivestockAnimals");

        builder.HasKey(la => la.Id);

        // Composite Unique Index to prevent duplicate rows for the same animal per household
        builder.HasIndex(la => new { la.LivestockId, la.AnimalTypeId })
            .IsUnique()
            .HasDatabaseName("IX_LivestockAnimals_Household_AnimalType");

        // Many-to-One Link back to Parent Livestock Table
        builder.HasOne(la => la.Livestock)
            .WithMany(l => l.Animals)
            .HasForeignKey(la => la.LivestockId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_LivestockAnimals_Livestock");

        // Master Lookup Link for Animal Type (Cow, Buffalo, etc.)
        builder.HasOne(la => la.AnimalType)
            .WithMany()
            .HasForeignKey(la => la.AnimalTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_LivestockAnimals_AnimalType");
    }
}