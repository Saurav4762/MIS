using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Wards;

public interface IWardService
{
	Task<Ward> CreateWardAsync(CreateWardDTO dto);
	Task<List<Ward>> GetAllWardsAsync();
	Task<List<Ward>> GetWardsByMunicipalityIdAsync(Guid municipalityId);
	Task<Ward> GetWardByIdAsync(Guid id);
	Task<Ward> UpdateWardAsync(Guid id, UpdateWardDTO dto);
	Task DeleteWardAsync(Guid id);
}
