using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AgricultureConfiguration : IEntityTypeConfiguration<Agriculture>
{
    public void Configure(EntityTypeBuilder<Agriculture> builder)
    {
        // Table Mapping
        builder.ToTable("Agriculture");

        // Primary Key
        builder.HasKey(a => a.Id);

        // Decimal Precision Setup for Land Area
        builder.Property(a => a.TotalArea)
            .HasPrecision(18, 4);

        // Bi-Directional One-to-One relationship with Family
        builder.HasOne(a => a.Family)
            .WithOne(f => f.Agriculture)
            .HasForeignKey<Agriculture>(a => a.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Agriculture_Family");

        // Single Selection Dropdown: Land Unit Lookup
        builder.HasOne(a => a.LandUnit)
            .WithMany()
            .HasForeignKey(a => a.LandUnitId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Agriculture_LandUnit");

        // Single Selection Dropdown: Ownership Status Lookup
        builder.HasOne(a => a.OwnershipStatus)
            .WithMany()
            .HasForeignKey(a => a.OwnershipStatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Agriculture_OwnershipStatus");

        // =======================================================================
        // MANY-TO-MANY BRIDGE CONFIGURATIONS FOR CHECKBOXES (Future Proofing)
        // =======================================================================

        // 1. Land Types Junction Table
        builder.HasMany(a => a.LandTypes)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AgricultureLandType",
                j => j.HasOne<OptionItem>().WithMany().HasForeignKey("LandTypeId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Agriculture>().WithMany().HasForeignKey("AgricultureId").OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable("AgricultureLandTypes")
            );

        // 2. Selected Crops Junction Table
        builder.HasMany(a => a.SelectedCrops)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AgricultureCrop",
                j => j.HasOne<OptionItem>().WithMany().HasForeignKey("CropId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Agriculture>().WithMany().HasForeignKey("AgricultureId").OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable("AgricultureCrops")
            );

        // 3. Equipments Junction Table
        builder.HasMany(a => a.Equipments)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AgricultureEquipment",
                j => j.HasOne<OptionItem>().WithMany().HasForeignKey("EquipmentId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Agriculture>().WithMany().HasForeignKey("AgricultureId").OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable("AgricultureEquipments")
            );

        // 4. Problems Faced Junction Table
        builder.HasMany(a => a.ProblemsFaced)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AgricultureProblem",
                j => j.HasOne<OptionItem>().WithMany().HasForeignKey("ProblemId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Agriculture>().WithMany().HasForeignKey("AgricultureId").OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable("AgricultureProblems")
            );
    }
}