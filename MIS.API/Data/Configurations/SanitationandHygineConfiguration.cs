using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class SanitationandHygineConfiguration : IEntityTypeConfiguration<SanitationandHygine>
{
    public void Configure(EntityTypeBuilder<SanitationandHygine> entity)
    {
        entity.HasKey(sh => sh.Id);

        entity.HasOne(s => s.SourceOfWater)
            .WithMany()
            .HasForeignKey(s => s.SourceOfWaterId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(s => s.TypeOfToiletOption)
            .WithMany()
            .HasForeignKey(s => s.TypeOfToiletId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(s => s.HandwashFacility)
            .WithMany() 
            .HasForeignKey(s => s.HandwashFacilityId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(sh => sh.Household)
            .WithOne(h => h.SanitationandHygine)
            .HasForeignKey<SanitationandHygine>(sh => sh.HouseholdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}