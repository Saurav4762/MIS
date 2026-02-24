using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class OptionItemConfiguration : IEntityTypeConfiguration<OptionItem>
{
    public void Configure(EntityTypeBuilder<OptionItem> entity)
    {
        entity.HasIndex(oi => new { oi.OptionListId, oi.Code }).IsUnique();
        entity.Property(oi => oi.Code).IsRequired();

        entity.Property(oi => oi.LabelEn).IsRequired();
        entity.Property(oi => oi.LabelNe).IsRequired();

        entity.HasOne(oi => oi.OptionList)
            .WithMany(ol => ol.OptionItems)
            .HasForeignKey(oi => oi.OptionListId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.Property(oi => oi.Extra).HasColumnType("jsonb");

        

    }

}