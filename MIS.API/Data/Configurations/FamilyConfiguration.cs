using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MIS.API.Models;

public class FamilyConfiguration  : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> entity)
    {
        entity.OwnsOne(e => e.Ethnicity);
        entity.OwnsOne(e => e.Religion);
        
        entity.HasOne(e=>e.Ethnicity)
            .WithMany(f=>f.Families)
            .HasForeignKey(e => e.EthnicityId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(e => e.Religion)
            .WithMany(f=>f.Families)
            .HasForeignKey(e => e.ReligionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(f=>f.HeadOfTheFamily)
            .WithMany()
            .HasForeignKey(f=>f.HeadOfTheFamilyId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}