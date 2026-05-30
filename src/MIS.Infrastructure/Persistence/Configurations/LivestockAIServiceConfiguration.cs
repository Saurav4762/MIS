using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class LivestockAIServiceConfiguration : IEntityTypeConfiguration<LivestockAIService>
{
    public void Configure(EntityTypeBuilder<LivestockAIService> builder)
    {
        builder.ToTable("LivestockAIServices");

        builder.HasKey(las => las.Id);

        // Configure Max Lengths for text fields to optimize DB storage
        builder.Property(las => las.AnimalName).HasMaxLength(150);
        builder.Property(las => las.SemenOrBullName).HasMaxLength(150);
        builder.Property(las => las.AIServiceDate).HasMaxLength(10); // Format: YYYY-MM-DD

        // Many-to-One Link back to Parent Livestock Table
        builder.HasOne(las => las.Livestock)
            .WithMany(l => l.AIServices)
            .HasForeignKey(las => las.LivestockId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_LivestockAIServices_Livestock");

        // Master Lookup Link: Animal Type
        builder.HasOne(las => las.AnimalType)
            .WithMany()
            .HasForeignKey(las => las.AnimalTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_LivestockAIServices_AnimalType");

        // Master Lookup Link: Birth History Status
        builder.HasOne(las => las.BirthHistory)
            .WithMany()
            .HasForeignKey(las => las.BirthHistoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_LivestockAIServices_BirthHistory");

        // Master Lookup Link: Current Status
        builder.HasOne(las => las.Status)
            .WithMany()
            .HasForeignKey(las => las.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_LivestockAIServices_Status");
    }
}