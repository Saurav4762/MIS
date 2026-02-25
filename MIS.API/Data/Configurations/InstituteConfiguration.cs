using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class InstituteConfiguration: IEntityTypeConfiguration<Institute>
{
  public void Configure(EntityTypeBuilder<Institute> entity)
  {
    entity.HasKey(e => e.Id);
    entity.HasOne(e => e.House)
          .WithOne(e => e.Institute)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
  }
}