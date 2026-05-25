using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MIS.Infrastructure.Persistence.Reports.Population;

public class PopulationReportService
{
    private readonly ApplicationDbContext _context;

    public PopulationReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChartDataResponse> GetPopulationByGenderAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Query: Count persons by gender in this municipality
        // Path: Person -> Family -> House -> Tole -> Ward -> Municipality
        var populationByGender = await (from person in _context.Set<Person>()
                                       join family in _context.Set<Family>() on person.Family!.Id equals family.Id into fam
                                       from family in fam.DefaultIfEmpty()
                                       join house in _context.Set<House>() on family.HouseId equals house.Id into hse
                                       from house in hse.DefaultIfEmpty()
                                       join tole in _context.Set<Tole>() on house.ToleId equals tole.Id into tls
                                       from tole in tls.DefaultIfEmpty()
                                       join ward in _context.Set<Ward>() on tole.WardId equals ward.Id into wrds
                                       from ward in wrds.DefaultIfEmpty()
                                       where ward != null && ward.MunicipalityId == municipalityId
                                       group person by person.Gender
                                       into g
                                       select new
                                       {
                                           Gender = g.Key,
                                           Count = g.Count()
                                       }).ToListAsync();

        var maleCount = populationByGender.FirstOrDefault(x => x.Gender != null && x.Gender.ToLower().Contains("male"))?.Count ?? 0;
        var femaleCount = populationByGender.FirstOrDefault(x => x.Gender != null && x.Gender.ToLower().Contains("female"))?.Count ?? 0;
        var otherCount = populationByGender
            .Where(x => x.Gender == null || (!x.Gender.ToLower().Contains("male") && !x.Gender.ToLower().Contains("female")))
            .Sum(x => (long)x.Count);

        var totalPopulation = maleCount + femaleCount + (int)otherCount;

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"Population by Gender - {municipality.NameEn}",
            Description = $"Total Population: {totalPopulation}",
            Labels = new List<string> { "Male", "Female", "Other" },
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Population Count",
                    Data = new List<int> { maleCount, femaleCount, (int)otherCount },
                    BackgroundColor = "#3498db,#e74c3c,#95a5a6",
                    BorderColor = "#2980b9,#c0392b,#7f8c8d"
                }
            },
            TotalRecords = totalPopulation
        };
    }

    public async Task<ChartDataResponse> GetPopulationByAgeGroupAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var today = DateTime.Today;

        // Get all persons in the municipality with their dates of birth
        var municipalityPersons = await (from person in _context.Set<Person>()
                                        join family in _context.Set<Family>() on person.Family!.Id equals family.Id into fam
                                        from family in fam.DefaultIfEmpty()
                                        join house in _context.Set<House>() on family.HouseId equals house.Id into hse
                                        from house in hse.DefaultIfEmpty()
                                        join tole in _context.Set<Tole>() on house.ToleId equals tole.Id into tls
                                        from tole in tls.DefaultIfEmpty()
                                        join ward in _context.Set<Ward>() on tole.WardId equals ward.Id into wrds
                                        from ward in wrds.DefaultIfEmpty()
                                        where ward != null && ward.MunicipalityId == municipalityId
                                        select new { person.Id, person.DateOfBirth }).ToListAsync();

        // Calculate age groups in memory
        var age0To5 = municipalityPersons
            .Count(p => CalculateAge(p.DateOfBirth, today) < 6);

        var age6To18 = municipalityPersons
            .Count(p => CalculateAge(p.DateOfBirth, today) >= 6 && CalculateAge(p.DateOfBirth, today) <= 18);

        var age19To60 = municipalityPersons
            .Count(p => CalculateAge(p.DateOfBirth, today) > 18 && CalculateAge(p.DateOfBirth, today) < 60);

        var age60Plus = municipalityPersons
            .Count(p => CalculateAge(p.DateOfBirth, today) >= 60);

        var totalPopulation = age0To5 + age6To18 + age19To60 + age60Plus;

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Population by Age Groups - {municipality.NameEn}",
            Description = $"Total Population: {totalPopulation}",
            Labels = new List<string> { "0-5 Years", "6-18 Years", "19-60 Years", "60+ Years" },
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = new List<int> { age0To5, age6To18, age19To60, age60Plus },
                    BackgroundColor = "#3498db",
                    BorderColor = "#2980b9"
                }
            },
            TotalRecords = totalPopulation
        };
    }

    public async Task<ChartDataResponse> GetPopulationByEducationAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Get municipality persons with educations
        var populationByEducation = await (from person in _context.Set<Person>()
                                          join family in _context.Set<Family>() on person.Family!.Id equals family.Id into fam
                                          from family in fam.DefaultIfEmpty()
                                          join house in _context.Set<House>() on family.HouseId equals house.Id into hse
                                          from house in hse.DefaultIfEmpty()
                                          join tole in _context.Set<Tole>() on house.ToleId equals tole.Id into tls
                                          from tole in tls.DefaultIfEmpty()
                                          join ward in _context.Set<Ward>() on tole.WardId equals ward.Id into wrds
                                          from ward in wrds.DefaultIfEmpty()
                                          where ward != null && ward.MunicipalityId == municipalityId && 
                                                person.Educations != null && person.Educations.Any()
                                          from education in person.Educations
                                          group education by education.Program
                                          into g
                                          select new
                                          {
                                              Program = g.Key,
                                              Count = g.Count()
                                          }).OrderByDescending(x => x.Count).ToListAsync();

        var totalEducated = populationByEducation.Sum(x => x.Count);

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Population by Education Level - {municipality.NameEn}",
            Description = $"Total with Education Records: {totalEducated}",
            Labels = populationByEducation.Select(x => x.Program ?? "Unknown").ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = populationByEducation.Select(x => x.Count).ToList(),
                    BackgroundColor = "#2ecc71",
                    BorderColor = "#27ae60"
                }
            },
            TotalRecords = totalEducated
        };
    }

    public async Task<ChartDataResponse> GetLiteracyRateAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Get all persons in municipality
        var municipalityPersonIds = await (from person in _context.Set<Person>()
                                          join family in _context.Set<Family>() on person.Family!.Id equals family.Id into fam
                                          from family in fam.DefaultIfEmpty()
                                          join house in _context.Set<House>() on family.HouseId equals house.Id into hse
                                          from house in hse.DefaultIfEmpty()
                                          join tole in _context.Set<Tole>() on house.ToleId equals tole.Id into tls
                                          from tole in tls.DefaultIfEmpty()
                                          join ward in _context.Set<Ward>() on tole.WardId equals ward.Id into wrds
                                          from ward in wrds.DefaultIfEmpty()
                                          where ward != null && ward.MunicipalityId == municipalityId
                                          select person.Id).ToListAsync();

        var personsWithEducation = await _context.Set<Person>()
            .Where(p => municipalityPersonIds.Contains(p.Id) &&
                        p.Educations != null && p.Educations.Any())
            .CountAsync();

        var totalPersons = municipalityPersonIds.Count;

        var literacyRate = totalPersons > 0 ? (decimal)(personsWithEducation * 100) / totalPersons : 0;

        return new ChartDataResponse
        {
            ChartType = "doughnut",
            Title = $"Literacy Rate - {municipality.NameEn}",
            Description = $"Literacy Rate: {literacyRate:F2}%",
            Labels = new List<string> { "Literate", "Illiterate" },
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = new List<int> { personsWithEducation, totalPersons - personsWithEducation },
                    BackgroundColor = "#f39c12,#e67e22",
                    BorderColor = "#d68910,#d35400"
                }
            },
            TotalRecords = totalPersons
        };
    }

    public async Task<ChartDataResponse> GetPopulationByGenderAndWardAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var populationByWard = await (from person in _context.Set<Person>()
                                      join family in _context.Set<Family>() on person.Family!.Id equals family.Id into fam
                                      from family in fam.DefaultIfEmpty()
                                      join house in _context.Set<House>() on family.HouseId equals house.Id into hse
                                      from house in hse.DefaultIfEmpty()
                                      join tole in _context.Set<Tole>() on house.ToleId equals tole.Id into tls
                                      from tole in tls.DefaultIfEmpty()
                                      join ward in _context.Set<Ward>() on tole.WardId equals ward.Id into wrds
                                      from ward in wrds.DefaultIfEmpty()
                                      where ward != null && ward.MunicipalityId == municipalityId
                                      group person by new { ward.Number, person.Gender }
                                      into g
                                      select new
                                      {
                                          WardNumber = g.Key.Number,
                                          Gender = g.Key.Gender,
                                          Count = g.Count()
                                      }).OrderBy(x => x.WardNumber).ToListAsync();

        var wards = populationByWard.Select(x => x.WardNumber).Distinct().OrderBy(x => x).ToList();
        var maleData = new List<int>();
        var femaleData = new List<int>();
        var otherData = new List<int>();

        foreach (var wardNumber in wards)
        {
            var wardData = populationByWard.Where(x => x.WardNumber == wardNumber).ToList();
            maleData.Add(wardData.FirstOrDefault(x => x.Gender != null && x.Gender.ToLower().Contains("male"))?.Count ?? 0);
            femaleData.Add(wardData.FirstOrDefault(x => x.Gender != null && x.Gender.ToLower().Contains("female"))?.Count ?? 0);
            otherData.Add(wardData.Where(x => x.Gender == null || (!x.Gender.ToLower().Contains("male") && !x.Gender.ToLower().Contains("female"))).Sum(x => x.Count));
        }

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Population by Gender and Ward - {municipality.NameEn}",
            Labels = wards.Select(w => $"Ward {w}").ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Male",
                    Data = maleData,
                    BackgroundColor = "#3498db"
                },
                new Dataset
                {
                    Label = "Female",
                    Data = femaleData,
                    BackgroundColor = "#e74c3c"
                },
                new Dataset
                {
                    Label = "Other",
                    Data = otherData,
                    BackgroundColor = "#95a5a6"
                }
            },
            TotalRecords = maleData.Sum() + femaleData.Sum() + otherData.Sum()
        };
    }

    public async Task<ChartDataResponse> GetPopulationByWardAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var populationByWard = await _context.Set<Person>()
            .Where(p => p.Family != null && p.Family.House != null &&
                        p.Family.House.Tole != null && p.Family.House.Tole.Ward != null &&
                        p.Family.House.Tole.Ward.MunicipalityId == municipalityId)
            .GroupBy(p => p.Family!.House!.Tole!.Ward!.Number)
            .Select(g => new
            {
                WardNumber = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Population by Ward - {municipality.NameEn}",
            Labels = populationByWard.Select(x => $"Ward {x.WardNumber}").ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Population",
                    Data = populationByWard.Select(x => x.Count).ToList(),
                    BackgroundColor = "#3498db"
                }
            },
            TotalRecords = populationByWard.Sum(x => x.Count)
        };
    }

    // ============ HELPER METHODS ============

    private int CalculateAge(DateTime dateOfBirth, DateTime referenceDate)
    {
        int age = referenceDate.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > referenceDate.AddYears(-age)) age--;
        return age;
    }
}
