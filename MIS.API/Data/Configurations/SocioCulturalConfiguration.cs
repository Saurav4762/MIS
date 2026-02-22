using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class SocioCulturalConfiguration : IEntityTypeConfiguration<SocioCultural>
{
    public void Configure(EntityTypeBuilder<SocioCultural> entity)
    {
        entity.HasKey(sc => sc.Id);
        
        entity.HasOne(sc => sc.Ethnicity)
            .WithMany()
            .HasForeignKey(sc => sc.EthnicityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(sc => sc.Religion)
            .WithMany()
            .HasForeignKey(sc => sc.ReligionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(sc => sc.ComLanguage)
            .WithMany()
            .HasForeignKey(sc => sc.ComLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(sc => sc.MotherTongue)
            .WithMany()
            .HasForeignKey(sc => sc.MothertongueId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(sc => sc.MajorFamilyOccupation)
            .WithMany()
            .HasForeignKey(sc => sc.MajorFamilyOccupationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}