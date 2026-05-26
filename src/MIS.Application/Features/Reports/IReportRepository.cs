namespace MIS.Application.Features.Reports;

public interface IReportRepository
{
    Task<int> GetTotalPersonsAsync(Guid municipalityId);
    Task<int> GetMaleCountAsync(Guid municipalityId);
    Task<int> GetFemaleCountAsync(Guid municipalityId);
    Task<int> GetOthersCountAsync(Guid municipalityId);
    Task<int> GetTotalHouseholdsAsync(Guid municipalityId);
    Task<int> GetAge16PlusCountAsync(Guid municipalityId);
    Task<int> GetLiterateCountAsync(Guid municipalityId);
}