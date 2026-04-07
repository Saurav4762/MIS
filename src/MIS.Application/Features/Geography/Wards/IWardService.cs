using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Wards;

public interface IWardService
{
	Task<WardDTO> CreateWardAsync(CreateWardDTO dto);
	Task<List<WardDTO>> GetAllWardsAsync();
	Task<List<WardDTO>> GetWardsByMunicipalityIdAsync(Guid municipalityId);
	Task<WardDTO> GetWardByIdAsync(Guid id);
	Task<WardDTO> UpdateWardAsync(Guid id, UpdateWardDTO dto);
	Task DeleteWardAsync(Guid id);
}
