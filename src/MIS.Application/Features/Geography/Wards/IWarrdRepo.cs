using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Wards;

public interface IWarrdRepo
{
	Task<Ward> CreateWardAsync(Ward ward);
	Task<List<Ward>> GetAllWardsAsync();
	Task<List<Ward>> GetWardsByMunicipalityIdAsync(Guid municipalityId);
	Task<Ward?> GetWardByIdAsync(Guid id);
	Task<Ward> UpdateWardAsync(Ward ward);
	Task<int> DeleteWardAsync(Guid id);
}
