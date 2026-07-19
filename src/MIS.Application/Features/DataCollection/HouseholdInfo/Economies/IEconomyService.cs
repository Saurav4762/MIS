namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;

public interface IEconomyService
{
    Task<EconomyDto> GetEconomyByFamilyIdAsync(Guid familyId);
    Task<EconomyDto> CreateEconomyAsync(CreateEconomyDto createEconomyDto);
    Task<EconomyDto> UpdateEconomyAsync(Guid familyId, EconomyDto economyDto);
}