using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class HealthConfiguration : IEntityTypeConfiguration<Health>
{
    public void Configure(EntityTypeBuilder<Health> builder)
    {
        // Table Name
        builder.ToTable("Health");

        // Primary Key
        builder.HasKey(h => h.Id);

        // Bi-Directional One-to-One relationship with Family
        builder.HasOne(h => h.Family)
            .WithOne(f => f.Health) 
            .HasForeignKey<Health>(h => h.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Health_Family");

        // Insurance Provider Lookups (Master Setup Option Item)
        builder.HasOne(h => h.InsuranceProvider)
            .WithMany()
            .HasForeignKey(h => h.InsuranceProviderId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Health_InsuranceProvider");

        // Many-to-Many Relationship Configuration for Section 03 (Checkboxes)
        // This automatically handles the middle join table for the selected illnesses
        builder.HasMany(h => h.ChronicIllnesses)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "HealthChronicIllness",
                j => j.HasOne<OptionItem>().WithMany().HasForeignKey("ChronicIllnessId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Health>().WithMany().HasForeignKey("HealthId").OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable("HealthChronicIllnesses")
            );
    }
}