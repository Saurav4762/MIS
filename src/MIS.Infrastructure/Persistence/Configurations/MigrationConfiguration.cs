using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class MigrationConfiguration : IEntityTypeConfiguration<Migration>
{
    public void Configure(EntityTypeBuilder<Migration> builder)
    {

        //Primary Key
        builder.HasKey(r => r.Id);

        // one to one
        builder.HasOne(r => r.Family)
            .WithMany(f => f.Migrations)
            .HasForeignKey(m => m.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Residence_Family");

        //Option Item Relation

        builder.HasOne(r => r.PreviousMunicipality)
            .WithMany()
            .HasForeignKey(m => m.PreviousMunicipalityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_PreviousMunicipality");

        builder.HasOne(r => r.ReasonOfMigration)
            .WithMany()
            .HasForeignKey(m => m.ReasonOfMigrationId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Residence_ReasonOfMigration");

    }
}