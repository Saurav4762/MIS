using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities.Identity;

namespace MIS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u=>u.Id);

        entity.HasIndex(u => u.FullName)
            .IsUnique();

        entity.Property(u => u.FullName)
        .IsRequired()
        .HasMaxLength(100);    

        entity.Property(u => u.PasswordHash)
            .IsRequired();

    }
}