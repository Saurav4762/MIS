using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        // Primary Key
        builder.HasKey(fm => fm.Id);

        // Required Properties
        builder.Property(fm => fm.FullNameEn).IsRequired();
        builder.Property(fm => fm.FullNameNe).IsRequired();
        builder.Property(fm => fm.DateOfBirth).IsRequired();
        builder.Property(fm => fm.MobileNumber).IsRequired();

        // Foreign Key: Family
        builder.HasOne(fm => fm.Family)
            .WithMany(f=>f.Members)
            .HasForeignKey(fm => fm.FamilyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_FamilyMember_Family");

        // Foreign Key: Gender (OptionItem)
        builder.HasOne(fm => fm.Gender)
            .WithMany()
            .HasForeignKey(fm => fm.GenderId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_Gender");

        // Foreign Key: Marital Status (OptionItem)
        builder.HasOne(fm => fm.MaritalStatus)
            .WithMany()
            .HasForeignKey(fm => fm.MaritalStatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_MaritalStatus");

        // Foreign Key: Relationship to Head (OptionItem)
        builder.HasOne(fm => fm.RelationshipToHead)
            .WithMany()
            .HasForeignKey(fm => fm.RelationshipToHeadId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_RelationshipToHead");
        // Foreign Key: ID Type (OptionItem)
        builder.HasOne(fm => fm.IdType)
            .WithMany()
            .HasForeignKey(fm => fm.IdTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_IdType");

        // Foreign Key: Education Level (OptionItem)
        builder.HasOne(fm => fm.EducationLevel)
            .WithMany()
            .HasForeignKey(fm => fm.EducationLevelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_EducationLevel");

        // Foreign Key: Occupation (OptionItem)
        builder.HasOne(fm => fm.Occupation)
            .WithMany()
            .HasForeignKey(fm => fm.OccupationId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_FamilyMember_Occupation");
        
        // Composite Index
        builder.HasIndex(fm => new { fm.FamilyId, fm.FullNameEn });
    }
}