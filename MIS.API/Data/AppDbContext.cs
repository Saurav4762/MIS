using Microsoft.EntityFrameworkCore;
using MIS.API.Models;

namespace MIS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Users> Users => Set<Users>();
    public DbSet<Roles> Roles => Set<Roles>();
}
