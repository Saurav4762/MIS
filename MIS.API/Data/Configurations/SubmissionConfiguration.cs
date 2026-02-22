using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> entity)
    {
        entity.HasOne(s => s.CreatedBy)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.SetNull);

        entity.Property(s => s.StartGeopoint).HasColumnType("geography (point)");
        entity.Property(s => s.RawSubmissionJson).HasColumnType("jsonb");
    }
    
}