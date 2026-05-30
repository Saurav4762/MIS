using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class EconomicConfiguration : IEntityTypeConfiguration<Economy>
{
    public void Configure(EntityTypeBuilder<Economy> builder)
    {
        // Table Name
        builder.ToTable("Economy");

        // Primary Key
        builder.HasKey(e => e.Id);

        // One-to-One relationship with Family (Enforces clean unique profile mapping)
        builder.HasOne(e => e.Family)
            .WithOne()
            .HasForeignKey<Economy>(e => e.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Economy_Family");

        // Master Setup Lookups (Restricts deleting options to prevent data corruption)
        builder.HasOne(e => e.ClassificationStatus)
            .WithMany()
            .HasForeignKey(e => e.ClassificationStatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Economy_ClassificationStatus");

        builder.HasOne(e => e.MainIncomeSource)
            .WithMany()
            .HasForeignKey(e => e.MainIncomeSourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Economy_MainIncomeSource");

        builder.HasOne(e => e.LoanSource)
            .WithMany()
            .HasForeignKey(e => e.LoanSourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Economy_LoanSource");

        // Explicit Decimal Precision (Strictly required for financial accuracy)
        const string decimalColumnType = "decimal(18,2)";
        
        builder.Property(e => e.FoodExpenditure).HasColumnType(decimalColumnType);
        builder.Property(e => e.EducationExpenditure).HasColumnType(decimalColumnType);
        builder.Property(e => e.HealthExpenditure).HasColumnType(decimalColumnType);
        builder.Property(e => e.ClothingExpenditure).HasColumnType(decimalColumnType);
        builder.Property(e => e.AgricultureExpenditure).HasColumnType(decimalColumnType);
        builder.Property(e => e.OtherExpenditure).HasColumnType(decimalColumnType);
    }
}