using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Areas;

public interface IAreaRepo
{
	Task<Area> CreateAreaAsync(Area area);
	Task<List<Area>> GetAllAreasAsync();
	Task<Area?> GetAreaByIdAsync(Guid id);
	Task<Area?> GetAreaByNumberAndDistrictIdAsync(int number, Guid districtId);
	Task<List<Area>> GetAreasByDistrictIdAsync(Guid districtId);
	Task<Area> UpdateAreaAsync(Area area);
	Task<int> DeleteAreaAsync(Guid id);
}