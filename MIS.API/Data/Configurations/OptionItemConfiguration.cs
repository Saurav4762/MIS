namespace MIS.API.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

public class OptionItemConfiguration : IEntityTypeConfiguration<OptionItem>
{
  public void Configure(EntityTypeBuilder<OptionItem> entity)
  {
    entity.HasKey(e => e.Id);

    entity.Property(e => e.LabelEn).IsRequired().HasMaxLength(200);
    entity.Property(e => e.LabelNe).IsRequired().HasMaxLength(200);


    entity.HasOne(e => e.OptionList)
          .WithMany(e => e.OptionItems)
          .HasForeignKey(e => e.OptionListId)
          .OnDelete(DeleteBehavior.Cascade);


  }
}