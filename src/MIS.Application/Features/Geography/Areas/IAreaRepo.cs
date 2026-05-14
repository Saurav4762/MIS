using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Areas;

public interface IAreaRepo
{
	Task<Area> CreateAreaAsync(Area area);
	Task<List<Area>> GetAllAreasAsync();
	Task<Area?> GetAreaByIdAsync(Guid id);
	Task<Area?> GetByCodeAsync(string code);
	Task<List<Area>> GetAreasByDistrictIdAsync(Guid districtId);
	Task<List<Area>> SearchAreasAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1);
	Task<Area> UpdateAreaAsync(Area area);
	Task<int> DeleteAreaAsync(Guid id);
}