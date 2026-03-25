using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Municipalities;

public interface IMunicipalityService
{
	Task<Municipality> CreateMunicipalityAsync(CreateMunicipalityDTO municipality);
	public Task<int> SeedMunicipalityAsync(IMunicipalitySeedDTO dto);
	Task<List<Municipality>> GetAllMunicipalitiesAsync();
	Task<Municipality> GetMunicipalityByIdAsync(Guid id);
	Task<Municipality> UpdateMunicipalityAsync(Guid id, UpdateMunicipalityDTO municipality);
	Task DeleteMunicipalityAsync(Guid id);
}
