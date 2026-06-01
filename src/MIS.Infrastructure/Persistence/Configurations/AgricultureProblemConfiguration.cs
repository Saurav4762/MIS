using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Infrastructure.Persistence.Configurations;

public class AgricultureProblemConfiguration : IEntityTypeConfiguration<AgricultureProblem>
{
    public void Configure(EntityTypeBuilder<AgricultureProblem> builder)
    {
        builder.ToTable("AgricultureProblems");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Details).HasMaxLength(1000).IsRequired(false);

        builder.HasOne(p => p.Agriculture)
            .WithMany(a => a.ProblemsFaced)
            .HasForeignKey(p => p.AgricultureId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AgricultureProblem_Agriculture");

        builder.HasOne(p => p.Problem)
            .WithMany()
            .HasForeignKey(p => p.ProblemId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_AgricultureProblem_OptionItem");

        builder.HasIndex(p => p.AgricultureId);
        builder.HasIndex(p => p.ProblemId);
    }
}