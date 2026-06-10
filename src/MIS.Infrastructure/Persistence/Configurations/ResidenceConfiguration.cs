using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class ResidenceConfiguration :IEntityTypeConfiguration<Residence>
{
    public void Configure(EntityTypeBuilder<Residence> builder)
    {
        // Table Name
        builder.ToTable("Residence");
        
        //Primary Key
        builder.HasKey(r => r.Id);
        
        // one to one
        builder.HasOne(r => r.Family)
            .WithOne(f => f.Residence)
            .HasForeignKey<Residence>(r => r.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Residence_Family");
        
        
        //Option Item Relation
        builder.HasOne(r=> r.ResidentType)
            .WithMany()
            .HasForeignKey(r => r.ResidentTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_ResidentType");

        builder.HasOne(r => r.LandOwnership)
            .WithMany()
            .HasForeignKey(r => r.LandOwnershipId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_LandOwnership");

        builder.HasOne(r => r.PreviousDistrict)
            .WithMany()
            .HasForeignKey(r => r.PreviousDistrictId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_PreviousDistrict");

        builder.HasOne(r => r.PreviousMunicipality)
            .WithMany()
            .HasForeignKey(r => r.PreviousMunicipalityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_PreviousMunicipality");

        builder.HasOne(r => r.ReasonOfMigration)
            .WithMany()
            .HasForeignKey(r => r.ReasonOfMigrationId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_ReasonOfMigration");
            
    }
}