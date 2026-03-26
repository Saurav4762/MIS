using Microsoft.EntityFrameworkCore;
using MIS.Domain.Entities.Identity;

namespace MIS.Infrastructure.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // User Management
    public DbSet<User> Users => Set<User>();


    // Lookup tables
    public DbSet<OptionList> OptionLists => Set<OptionList>();
    public DbSet<OptionItem> OptionItems => Set<OptionItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
