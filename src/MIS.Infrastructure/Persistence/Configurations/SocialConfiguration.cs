using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class SocialConfiguration : IEntityTypeConfiguration<Social>
{
    public void Configure(EntityTypeBuilder<Social> builder)
    {
        // Table Name
        builder.ToTable("Social");

        // Primary Key
        builder.HasKey(s => s.Id);

        // ==================== One-to-One Relationship ====================
        // Foreign Key: Family (One Family has One Social record)
        builder.HasOne(s => s.Family)
            .WithOne(f => f.Social)                    // One-to-One
            .HasForeignKey<Social>(s => s.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Social_Family");

        // ==================== Other Relationships ====================

        // Foreign Key: Member
        builder.HasOne(s => s.Member)
            .WithMany()
            .HasForeignKey(s => s.MemberId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Social_Member");

        // Foreign Key: Ethnicity
        builder.HasOne(s => s.Ethnicity)
            .WithMany()
            .HasForeignKey(s => s.EthnicityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Social_Ethnicity");

        // Foreign Key: Religion
        builder.HasOne(s => s.Religion)
            .WithMany()
            .HasForeignKey(s => s.ReligionId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Social_Religion");

        // Foreign Key: MotherTongue
        builder.HasOne(s => s.MotherTongue)
            .WithMany()
            .HasForeignKey(s => s.MotherTongueId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Social_MotherTongue");

        // Foreign Key: CommonLanguage
        builder.HasOne(s => s.CommonLanguage)
            .WithMany()
            .HasForeignKey(s => s.CommonLanguageId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Social_CommonLanguage");
    }
}