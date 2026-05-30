using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class DisasterConfiguration : IEntityTypeConfiguration<Disaster>
{
    public void Configure(EntityTypeBuilder<Disaster> builder)
    {
        // Table Name
        builder.ToTable("Disasters");

        // Primary Key
        builder.HasKey(d => d.Id);

        // One-to-One Relationship with Family (Matches your Residence style)
        // Note: If you add 'public Disaster? Disaster { get; set; }' to your Family entity later,
        // change .WithOne() to .WithOne(f => f.Disaster)
        builder.HasOne(d => d.Family)
            .WithOne() 
            .HasForeignKey<Disaster>(d => d.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Disasters_Family");

        // Option Item Lookup Relation
        builder.HasOne(d => d.DisasterType)
            .WithMany()
            .HasForeignKey(d => d.DisasterTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Disasters_DisasterType");

        // Property Limit Optimization (Prevents NVARCHAR(MAX) column bloat)
        builder.Property(d => d.DisasterDescription)
            .HasMaxLength(500);
    }
}