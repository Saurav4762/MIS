using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Districts;

public interface IDistrictService
{
	Task<DistrictDTO> CreateDistrictAsync(CreateDistrictDTO district);
	Task<List<DistrictDTO>> GetAllDistrictsAsync();
	Task<DistrictDTO> GetDistrictByIdAsync(Guid id);
	Task<List<DistrictDTO>> GetDistrictsByProvinceIdAsync(Guid provinceId);
	Task<DistrictDTO> UpdateDistrictAsync(Guid id, UpdateDistrictDTO district);
	Task DeleteDistrictAsync(Guid id);
	Task<List<DistrictDTO>> SearchDistrictsAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1);
}
