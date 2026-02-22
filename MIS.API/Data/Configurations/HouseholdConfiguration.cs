using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
{
    public void Configure(EntityTypeBuilder<Household> entity)
    {
        entity.HasOne(h => h.HouseInfo)
            .WithMany(hh => hh.Households)
            .HasForeignKey(h => h.HouseInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}