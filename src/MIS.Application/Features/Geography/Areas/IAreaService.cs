using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Areas;

public interface IAreaService
{
	Task<AreaDTO> CreateAreaAsync(CreateAreaDTO area);
	Task<List<AreaDTO>> GetAllAreasAsync();
	Task<AreaDTO> GetAreaByIdAsync(Guid id);
	Task<List<AreaDTO>> GetAreasByDistrictIdAsync(Guid districtId);
	Task<AreaDTO> UpdateAreaAsync(Guid id, UpdateAreaDTO area);
	Task DeleteAreaAsync(Guid id);
}