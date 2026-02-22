using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.API.Models;

namespace MIS.API.Data.Configurations;

public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>

{
    public void Configure(EntityTypeBuilder<AppUserRole> entity)
    {
        entity.HasKey(aur => new { aur.AppUserId, aur.AppRoleId });
    }
}