using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Geography;

namespace MIS.Infrastructure.Persistence.Configurations;

public class Municipalityconfiguration : IEntityTypeConfiguration<Municipality>

{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.HasOne(m => m.District)
            .WithMany(d => d.Municipalities)
            .HasForeignKey(m => m.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(m => m.Code)
            .IsUnique();

        builder
            .HasIndex(m => m.Email)
            .IsUnique();

        builder
            .HasIndex(m => m.PhoneNo)
            .IsUnique();
    }
}