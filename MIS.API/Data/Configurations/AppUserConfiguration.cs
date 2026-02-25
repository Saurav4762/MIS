using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MIS.API.Models;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> entity)
    {
        entity.HasKey(u=>u.Id);

        entity.HasIndex(u => u.FullName)
            .IsUnique();

        entity.Property(u => u.FullName)
        .IsRequired()
        .HasMaxLength(100);    

        entity.Property(u => u.PasswordHash)
            .IsRequired();

        //Reltionship :Appuser -> AppUserRole
        entity.HasMany(u=> u.AppUserRoles)
        .WithOne(ur => ur.AppUser)
        .HasForeignKey(ur => ur.AppUserId);   
    }
}