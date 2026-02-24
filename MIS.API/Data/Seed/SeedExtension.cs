using Microsoft.EntityFrameworkCore;
using MIS.API.Models;

namespace MIS.API.Data.Seed
{
    public static class SeedExtensions
    {
        public static void SeedMasterData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OptionList>()
                .HasData(OptionListSeed.Data);

            modelBuilder.Entity<OptionItem>()
                .HasData(OptionItemSeed.Data);
        }
    }
}