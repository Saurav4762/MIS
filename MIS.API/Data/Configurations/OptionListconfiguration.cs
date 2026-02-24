using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class OptionListconfiguration : IEntityTypeConfiguration<OptionList>
{
    public void Configure(EntityTypeBuilder<OptionList> entity)
    {
        entity.HasIndex(ol => ol.Code).IsUnique();
        entity.Property(ol => ol.Code).IsRequired();

        entity.Property(ol => ol.LabelEn).IsRequired();
        entity.Property(ol => ol.LabelNe).IsRequired();



    }
}