using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MIS.Infrastructure.Persistence.Configurations;

public class OptionListConfiguration : IEntityTypeConfiguration<OptionList>
{
  public void Configure(EntityTypeBuilder<OptionList> entity)
  {
    entity.HasKey(e => e.Id);

    entity.Property(e => e.LabelEn).IsRequired().HasMaxLength(200);
    entity.Property(e => e.LabelNe).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Key).IsRequired().HasMaxLength(100);

  }
}