using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class FamilyDecisionConfiguration : IEntityTypeConfiguration<FamilyDecision>
{
    public void Configure(EntityTypeBuilder<FamilyDecision> entity)
    {
        entity.HasOne(fd => fd.Household)
            .WithOne(h => h.FamilyDecision)
            .HasForeignKey<FamilyDecision>(fd => fd.HouseholdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}