namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;

using MIS.Domain.Entities.DataCollection.HouseholdInfo;

public interface IEconomyRepo
{
  Task<Economy?> GetEconomyByFamilyIdAsync(Guid familyId);
  Task<Economy> CreateEconomyAsync(Economy economy);
  Task<Economy> UpdateEconomyAsync(Guid familyId, Economy economy);
}