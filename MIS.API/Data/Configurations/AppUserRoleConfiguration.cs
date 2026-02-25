using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> entity)
    {
        entity.HasKey(ur => new { ur.AppUserId, ur.AppRoleId });

        entity.HasOne(ur => ur.AppUser)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(ur => ur.AppUserId);

        entity.HasOne(ur => ur.AppRole)
            .WithMany(r=> r.AppUserRoles)
            .HasForeignKey(ur => ur.AppRoleId);
    }
}