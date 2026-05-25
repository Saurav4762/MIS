using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Infrastructure.Persistence.Data;
using MIS.Infrastructure.Persistence.Reports.DataQuality;
using MIS.Infrastructure.Persistence.Reports.FamilyReports;
using MIS.Infrastructure.Persistence.Reports.Geographic;
using MIS.Infrastructure.Persistence.Reports.Households;
using MIS.Infrastructure.Persistence.Reports.Population;

namespace MIS.Infrastructure.Persistence.Reports;

/// <summary>
/// Orchestrator service for all report generation
/// Routes requests to appropriate specialized report services
/// </summary>
public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    private readonly PopulationReportService _populationReportService;
    private readonly HouseholdReportService _householdReportService;
    private readonly FamilyReportService _familyReportService;
    private readonly GeographicReportService _geographicReportService;
    private readonly DataQualityReportService _dataQualityReportService;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
        _populationReportService = new PopulationReportService(context);
        _householdReportService = new HouseholdReportService(context);
        _familyReportService = new FamilyReportService(context);
        _geographicReportService = new GeographicReportService(context);
        _dataQualityReportService = new DataQualityReportService(context);
    }

    // ============ POPULATION REPORTS ============

    public async Task<ChartDataResponse> GetPopulationByGenderAsync(Guid municipalityId) =>
        await _populationReportService.GetPopulationByGenderAsync(municipalityId);

    public async Task<ChartDataResponse> GetPopulationByAgeGroupAsync(Guid municipalityId) =>
        await _populationReportService.GetPopulationByAgeGroupAsync(municipalityId);

    public async Task<ChartDataResponse> GetPopulationByEducationAsync(Guid municipalityId) =>
        await _populationReportService.GetPopulationByEducationAsync(municipalityId);

    public async Task<ChartDataResponse> GetLiteracyRateAsync(Guid municipalityId) =>
        await _populationReportService.GetLiteracyRateAsync(municipalityId);

    public async Task<ChartDataResponse> GetPopulationByGenderAndWardAsync(Guid municipalityId) =>
        await _populationReportService.GetPopulationByGenderAndWardAsync(municipalityId);

    public async Task<ChartDataResponse> GetPopulationByWardAsync(Guid municipalityId) =>
        await _populationReportService.GetPopulationByWardAsync(municipalityId);

    // ============ HOUSEHOLD REPORTS ============

    public async Task<ChartDataResponse> GetHouseholdsByWardAsync(Guid municipalityId) =>
        await _householdReportService.GetHouseholdsByWardAsync(municipalityId);

    public async Task<ChartDataResponse> GetHouseholdsByOwnershipAsync(Guid wardId) =>
        await _householdReportService.GetHouseholdsByOwnershipAsync(wardId);

    public async Task<ChartDataResponse> GetAverageFamilySizeAsync(Guid municipalityId) =>
        await _householdReportService.GetAverageFamilySizeAsync(municipalityId);

    public async Task<ChartDataResponse> GetHouseholdTrendAsync(Guid municipalityId) =>
        await _householdReportService.GetHouseholdTrendAsync(municipalityId);

    public async Task<ChartDataResponse> GetHouseTypeDistributionAsync(Guid wardId) =>
        await _householdReportService.GetHouseTypeDistributionAsync(wardId);

    public async Task<ChartDataResponse> GetRoofTypeDistributionAsync(Guid wardId) =>
        await _householdReportService.GetRoofTypeDistributionAsync(wardId);

    public async Task<ChartDataResponse> GetWallMaterialDistributionAsync(Guid wardId) =>
        await _householdReportService.GetWallMaterialDistributionAsync(wardId);

    public async Task<ChartDataResponse> GetLandTypeDistributionAsync(Guid wardId) =>
        await _householdReportService.GetLandTypeDistributionAsync(wardId);

    public async Task<ChartDataResponse> GetHouseQualityAsync(Guid wardId) =>
        await _householdReportService.GetHouseQualityAsync(wardId);

    // ============ FAMILY REPORTS ============

    public async Task<ChartDataResponse> GetFamiliesByEthnicityAsync(Guid municipalityId) =>
        await _familyReportService.GetFamiliesByEthnicityAsync(municipalityId);

    public async Task<ChartDataResponse> GetFamiliesByReligionAsync(Guid municipalityId) =>
        await _familyReportService.GetFamiliesByReligionAsync(municipalityId);

    // ============ GEOGRAPHIC REPORTS ============

    public async Task<ChartDataResponse> GetGeographicCoverageAsync(Guid municipalityId) =>
        await _geographicReportService.GetGeographicCoverageAsync(municipalityId);

    public async Task<ChartDataResponse> GetWardHierarchyAsync(Guid municipalityId) =>
        await _geographicReportService.GetWardHierarchyAsync(municipalityId);

    // ============ DATA QUALITY REPORTS ============

    public async Task<ChartDataResponse> GetDataQualityMetricsAsync(Guid municipalityId) =>
        await _dataQualityReportService.GetDataQualityMetricsAsync(municipalityId);

    public async Task<ChartDataResponse> GetSubmissionStatusAsync(Guid municipalityId) =>
        await _dataQualityReportService.GetSubmissionStatusAsync(municipalityId);

    public async Task<ChartDataResponse> GetDataCompletenessAsync(Guid municipalityId) =>
        await _dataQualityReportService.GetDataCompletenessAsync(municipalityId);

    public async Task<DashboardStatsResponse> GetDashboardStatsAsync(Guid municipalityId)
    {
        // Count persons directly
        var totalPopulation = await _context.Set<Person>().CountAsync();
        var male = await _context.Set<Person>()
            .CountAsync(p => p.Gender != null && p.Gender.ToLower() =="male");
        var female = await _context.Set<Person>()
            .CountAsync(p => p.Gender != null && p.Gender.ToLower()== "female");
        var others = await _context.Set<Person>()
            .CountAsync(p => p.Gender == null ||
                             (p.Gender.ToLower() != "male" && p.Gender.ToLower() != "female"));

        // Use Family as households since House table doesn't exist yet
        var totalHouseholds = await _context.Set<Family>().CountAsync();

        // Age 16+
        var cutoff = DateTime.UtcNow.AddYears(-16);
        var age16Plus = await _context.Set<Person>()
            .CountAsync(p => p.DateOfBirth <= cutoff);

        // Literate (has education records)
        var literate = await _context.Set<Person>()
            .CountAsync(p => p.Educations != null && p.Educations.Any());

        return new DashboardStatsResponse
        {
            PopulationByGender = new PopulationByGender
            {
                Male = male,
                Female = female,
                Others = others
            },
            TotalHouseholds = totalHouseholds,
            TotalPopulation = totalPopulation,
            AgeGroup16Plus = age16Plus,
            Literate = literate,
            Jobless = 0,
            Timestamp = DateTime.UtcNow.ToString("o")
        };
    }
}
