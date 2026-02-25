using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class ToleConfiguration : IEntityTypeConfiguration<Tole>
{
    public void Configure(EntityTypeBuilder<Tole> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.WardId, t.Code }).IsUnique();
        builder.Property(t => t.Code).IsRequired();
        builder.Property(t => t.Name).IsRequired();

        builder.HasOne(t => t.Ward)
            .WithMany(w => w.Toles)
            .HasForeignKey(t => t.WardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
