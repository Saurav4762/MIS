using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AgricultureEquipmentConfiguration : IEntityTypeConfiguration<AgricultureEquipment>
{
    public void Configure(EntityTypeBuilder<AgricultureEquipment> builder)
    {
        builder.ToTable("AgricultureEquipments");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Condition).HasMaxLength(200).IsRequired(false);

        builder.HasOne(e => e.Agriculture)
            .WithMany(a => a.Equipments)
            .HasForeignKey(e => e.AgricultureId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AgricultureEquipment_Agriculture");

        builder.HasOne(e => e.Equipment)
            .WithMany()
            .HasForeignKey(e => e.EquipmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_AgricultureEquipment_OptionItem");

        builder.HasIndex(e => e.AgricultureId);
        builder.HasIndex(e => e.EquipmentId);
    }
}