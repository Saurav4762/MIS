using Microsoft.EntityFrameworkCore;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.DataCollection.HouseInfo;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Entities.Submissions;
using MIS.Infrastructure.Persistence;
using MIS.Infrastructure.Persistence.Data; // adjust to your actual DbContext namespace

namespace MIS.DevTools;

public static class TestFamilySeeder
{
    public static async Task<(Guid houseId, Guid submissionId)> SeedTestFamilyChain(ApplicationDbContext context)
    {
        
            var existingHouse = await context.Houses
                .FirstOrDefaultAsync(h => h.HouseNumber == "H-001");

            if (existingHouse != null)
            {
                Console.WriteLine($"Test data already exists. House.Id = {existingHouse.Id}, Submission.Id = {existingHouse.SubmissionId}");
                return (existingHouse.Id, existingHouse.SubmissionId);
            }

        // --- Geography chain ---
        var province = new Province
        {
            Code = "P11",
            NameEn = "Test Province",
            NameNe = "परीक्षण प्रदेश"
        };

        var district = new District
        {
            Code = "D110",
            NameEn = "Test District",
            NameNe = "परीक्षण जिल्ला",
            Province = province
        };

        var area = new Area
        {
            Number = 1,
            District = district
        };

        var municipality = new Municipality
        {
            Code = "M1",
            NameEn = "Test Municipality",
            NameNe = "परीक्षण नगरपालिका",
            HeadExecutiveNameEn = "Test Executive",
            HeadExecutiveNameNe = "परीक्षण प्रमुख",
            Email = "test@muni.gov.np",
            PhoneNo = "9800000000",
            Website = "https://test-muni.gov.np",
            Area = area
        };

        var ward = new Ward
        {
            Number = 1,
            Code = 1,
            RepresentativeNameEn = "Test Rep",
            RepresentativeNameNe = "परीक्षण प्रतिनिधि",
            PhoneNo = "9800000001",
            Email = "ward1@test.gov.np",
            Municipality = municipality
        };

        var tole = new Tole
        {
            Code = "T11",
            Name = "Test Tole",
            Ward = ward
        };

        // --- Option lists/items (House needs 4: HouseType, LandType, RoofType, WallType) ---
        var houseTypeList = new OptionList { Key = "house-type", LabelEn = "House Type", LabelNe = "घरको प्रकार" };
        var landTypeList = new OptionList { Key = "land-type", LabelEn = "Land Type", LabelNe = "जमिनको प्रकार" };
        var roofTypeList = new OptionList { Key = "roof-type", LabelEn = "Roof Type", LabelNe = "छानाको प्रकार" };
        var wallTypeList = new OptionList { Key = "wall-type", LabelEn = "Wall Type", LabelNe = "भित्ताको प्रकार" };

        var houseTypeOption = new OptionItem { LabelEn = "Concrete", LabelNe = "कंक्रिट", OptionList = houseTypeList };
        var landTypeOption = new OptionItem { LabelEn = "Flat", LabelNe = "समतल", OptionList = landTypeList };
        var roofTypeOption = new OptionItem { LabelEn = "Tin", LabelNe = "जस्ता", OptionList = roofTypeList };
        var wallTypeOption = new OptionItem { LabelEn = "Brick", LabelNe = "इँटा", OptionList = wallTypeList };

        
        
        

        // --- Submission ---
        var submission = new Submission
        {
            Status = SubmissionStatus.Draft,
            SubmittedAt = DateTime.UtcNow,
            SubmittedById = Guid.NewGuid() // no nav property on Submission side either — likely unconstrained; real User.Id if you have one seeded
        };

        // --- House ---
        var house = new House
        {
            HouseNumber = "H-005",
            Location = "Test Location",
            ImageId = Guid.NewGuid(), // no nav property — likely unconstrained
            Tole = tole,
            Ward = ward,
            HouseType = houseTypeOption,
            LandType = landTypeOption,
            RoofType = roofTypeOption,
            WallType = wallTypeOption
        };

        context.Add(submission);
        context.Add(house);
        await context.SaveChangesAsync(); // EF resolves Province→...→House graph AND generates all GUIDs in one shot

        // House doesn't have a Submission navigation property, so wire the FK manually after both have real Ids
        house.SubmissionId = submission.Id;
        await context.SaveChangesAsync();

        Console.WriteLine($"House.Id = {house.Id}");
        Console.WriteLine($"Submission.Id = {submission.Id}");

        return (house.Id, submission.Id);
    }
    
        
    public static async Task<Dictionary<string, Guid>> SeedMemberOptionItems(ApplicationDbContext context)
{
    // Reuse if already seeded (idempotency, same pattern as before)
    var existing = await context.OptionItems
        .Include(oi => oi.OptionList)
        .Where(oi => oi.OptionList.Key.StartsWith("test-"))
        .ToListAsync();

    if (existing.Any())
    {
        Console.WriteLine("Member option items already exist, reusing.");
        return BuildLookup(existing);
    }

    var genderList = new OptionList { Key = "test-gender", LabelEn = "Gender", LabelNe = "लिङ्ग" };
    var maritalList = new OptionList { Key = "test-marital-status", LabelEn = "Marital Status", LabelNe = "वैवाहिक स्थिति" };
    var relationList = new OptionList { Key = "test-relationship-to-head", LabelEn = "Relationship To Head", LabelNe = "मुख्यसँगको सम्बन्ध" };
    var idTypeList = new OptionList { Key = "test-id-type", LabelEn = "ID Type", LabelNe = "परिचयपत्र प्रकार" };
    var educationList = new OptionList { Key = "test-education-level", LabelEn = "Education Level", LabelNe = "शैक्षिक स्तर" };
    var occupationList = new OptionList { Key = "test-occupation", LabelEn = "Occupation", LabelNe = "पेशा" };

    var items = new List<OptionItem>
    {
        new() { LabelEn = "Male", LabelNe = "पुरुष", OptionList = genderList },
        new() { LabelEn = "Female", LabelNe = "महिला", OptionList = genderList },

        new() { LabelEn = "Married", LabelNe = "विवाहित", OptionList = maritalList },
        new() { LabelEn = "Unmarried", LabelNe = "अविवाहित", OptionList = maritalList },

        new() { LabelEn = "Head", LabelNe = "मुख्य", OptionList = relationList },
        new() { LabelEn = "Spouse", LabelNe = "पति/पत्नी", OptionList = relationList },

        new() { LabelEn = "Citizenship", LabelNe = "नागरिकता", OptionList = idTypeList },
        new() { LabelEn = "Passport", LabelNe = "राहदानी", OptionList = idTypeList },

        new() { LabelEn = "Bachelor's", LabelNe = "स्नातक", OptionList = educationList },
        new() { LabelEn = "None", LabelNe = "कुनै छैन", OptionList = educationList },

        new() { LabelEn = "Farmer", LabelNe = "किसान", OptionList = occupationList },
        new() { LabelEn = "Student", LabelNe = "विद्यार्थी", OptionList = occupationList },
    };

    context.AddRange(items);
    await context.SaveChangesAsync();

    Console.WriteLine("Seeded member option items:");
    foreach (var item in items)
    {
        Console.WriteLine($"  {item.OptionList.Key} / {item.LabelEn} => {item.Id}");
    }

    return BuildLookup(items);
}

private static Dictionary<string, Guid> BuildLookup(List<OptionItem> items)
{
    // Grabs the FIRST item per list (e.g. "Male", "Married", "Head"...) for quick payload use
    return items
        .GroupBy(i => i.OptionList.Key)
        .ToDictionary(g => g.Key, g => g.First().Id);
}
}