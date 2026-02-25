using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> entity)
    {
        entity.HasIndex(r => r.RoleCode)
            .IsUnique();

        entity.Property(r => r.RoleCode)
            .IsRequired();

        entity.Property(r => r.RoleName)
            .IsRequired();
    }
}