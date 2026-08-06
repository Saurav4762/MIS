using Microsoft.EntityFrameworkCore;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.DevTools;

public static class TestSocialSeeder
{
    public static async Task<Dictionary<string, Guid>> SeedSocialOptionItems(ApplicationDbContext context)
    {
        var existing = await context.OptionItems
            .Include(oi => oi.OptionList)
            .Where(oi => oi.OptionList.Key.StartsWith("test-social-"))
            .ToListAsync();

        if (existing.Any())
        {
            Console.WriteLine("Social option items already exist, reusing.");
            return BuildLookup(existing);
        }

        var ethnicityList = new OptionList { Key = "test-social-ethnicity", LabelEn = "Ethnicity", LabelNe = "जातीयता" };
        var religionList = new OptionList { Key = "test-social-religion", LabelEn = "Religion", LabelNe = "धर्म" };
        var motherTongueList = new OptionList { Key = "test-social-mother-tongue", LabelEn = "Mother Tongue", LabelNe = "मातृभाषा" };
        var commonLangList = new OptionList { Key = "test-social-common-language", LabelEn = "Common Language", LabelNe = "साझा भाषा" };

        var items = new List<OptionItem>
        {
            new() { LabelEn = "Brahmin", LabelNe = "ब्राह्मण", OptionList = ethnicityList },
            new() { LabelEn = "Hindu", LabelNe = "हिन्दू", OptionList = religionList },
            new() { LabelEn = "Nepali", LabelNe = "नेपाली", OptionList = motherTongueList },
            new() { LabelEn = "Nepali", LabelNe = "नेपाली", OptionList = commonLangList },
        };

        context.AddRange(items);
        await context.SaveChangesAsync();

        Console.WriteLine("Seeded social option items:");
        foreach (var item in items)
        {
            Console.WriteLine($"  {item.OptionList.Key} / {item.LabelEn} => {item.Id}");
        }

        return BuildLookup(items);
    }

    public static async Task<Guid?> GetAnyFamilyIdWithoutSocial(ApplicationDbContext context)
    {
        var family = await context.Families
            .Where(f => f.Social == null)
            .FirstOrDefaultAsync();

        if (family == null)
        {
            Console.WriteLine("No Family without an existing Social record was found.");
            return null;
        }

        Console.WriteLine($"Family available for Social testing: {family.Id}");
        return family.Id;
    }

    private static Dictionary<string, Guid> BuildLookup(List<OptionItem> items)
    {
        return items
            .GroupBy(i => i.OptionList.Key)
            .ToDictionary(g => g.Key, g => g.First().Id);
    }
}