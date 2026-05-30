using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class LivestockConfiguration : IEntityTypeConfiguration<Livestock>
{
    public void Configure(EntityTypeBuilder<Livestock> builder)
    {
        builder.ToTable("Livestock");

        builder.HasKey(l => l.Id);

        // Bi-Directional One-to-One relationship with Family
        builder.HasOne(l => l.Family)
            .WithOne(f => f.Livestock)
            .HasForeignKey<Livestock>(l => l.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Livestock_Family");
    }
}