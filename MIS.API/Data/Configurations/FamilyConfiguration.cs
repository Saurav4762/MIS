using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MIS.API.Models;

public class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> entity)
    {
        entity.HasOne<Ethnicity>(f => f.Ethnicity)
            .WithMany(e => e.Families)
            .HasForeignKey(f => f.EthnicityId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<Religion>(f => f.Religion)
            .WithMany(r => r.Families)
            .HasForeignKey(f => f.ReligionId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(f => f.HeadOfTheFamily)
            .WithOne(p => p.Family)
            .HasForeignKey<Family>(f => f.HeadOfTheFamilyId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}