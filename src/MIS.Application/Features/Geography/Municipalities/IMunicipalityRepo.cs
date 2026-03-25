using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Municipalities;

public interface IMunicipalityRepo
{
	Task<Municipality> CreateMunicipalityAsync(Municipality municipality);
	Task<int> BulkInsertAsync(List<Municipality> entities);
	Task<List<Municipality>> GetAllMunicipalitiesAsync();
	Task<Municipality?> GetMunicipalityByIdAsync(Guid id);
	Task<Municipality> UpdateMunicipalityAsync(Municipality municipality);
	Task<int> DeleteMunicipalityAsync(Guid id);
}
