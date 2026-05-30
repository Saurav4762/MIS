namespace MIS.Application.Features.Reports;

public record ChartItem(string Label, int Value);

public interface IReportRepository
{
    Task<int> GetTotalPersonsAsync(Guid municipalityId);
    Task<int> GetMaleCountAsync(Guid municipalityId);
    Task<int> GetFemaleCountAsync(Guid municipalityId);
    Task<int> GetOthersCountAsync(Guid municipalityId);
    Task<int> GetTotalHouseholdsAsync(Guid municipalityId);
    Task<int> GetAge16PlusCountAsync(Guid municipalityId);
    Task<int> GetLiterateCountAsync(Guid municipalityId);
    Task<List<ChartItem>> GetAgeGroupDistributionAsync();
    Task<List<ChartItem>> GetBloodGroupDistributionAsync();
    Task<List<ChartItem>> GetHouseTypeDistributionAsync();
    Task<List<ChartItem>> GetRoofTypeDistributionAsync();
    Task<List<ChartItem>> GetWallTypeDistributionAsync();
    Task<List<ChartItem>> GetLandTypeDistributionAsync();
    Task<List<ChartItem>> GetFamilyByEthnicityAsync();
    Task<List<ChartItem>> GetFamilyByReligionAsync();
    Task<List<ChartItem>> GetEducationProgramsAsync();
    Task<List<ChartItem>> GetWardHouseholdCountAsync();
}