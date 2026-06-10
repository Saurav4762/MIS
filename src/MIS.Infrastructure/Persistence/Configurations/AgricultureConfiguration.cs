using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AgricultureCropConfiguration : IEntityTypeConfiguration<AgricultureCrop>
{
    public void Configure(EntityTypeBuilder<AgricultureCrop> builder)
    {
        builder.ToTable("AgricultureCrops");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.AreaInHectares).HasPrecision(18,4).IsRequired(false);
        builder.Property(c => c.EstimatedYield).HasPrecision(18,4).IsRequired(false);
        builder.Property(c => c.Notes).HasMaxLength(1000).IsRequired(false);

        // FK -> Agriculture
        builder.HasOne(c => c.Agriculture)
            .WithMany(a => a.SelectedCrops)
            .HasForeignKey(c => c.AgricultureId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AgricultureCrop_Agriculture");

        // FK -> OptionItem (Crop)
        builder.HasOne(c => c.Crop)
            .WithMany()
            .HasForeignKey(c => c.CropId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_AgricultureCrop_Crop");

        // Indexes for fast lookups
        builder.HasIndex(c => c.AgricultureId);
        builder.HasIndex(c => c.CropId);
    }
}