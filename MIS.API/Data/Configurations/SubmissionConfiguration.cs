using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
  public void Configure(EntityTypeBuilder<Submission> entity)
  {
    entity.HasKey(e => e.Id);
    entity.HasOne(e => e.SubmittedBy)
          .WithMany(e => e.Submissions)
          .HasForeignKey(e => e.SubmittedById)
          .OnDelete(DeleteBehavior.Restrict);
  }

}