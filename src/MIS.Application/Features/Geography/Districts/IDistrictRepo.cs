using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Districts;

public interface IDistrictRepo
{
	Task<District> CreateDistrictAsync(District district);
	Task<List<District>> GetAllDistrictsAsync();
	Task<District?> GetDistrictByIdAsync(Guid id);
	Task<District?> GetByCodeAsync(string code);
	Task<List<District>> GetDistrictsByProvinceIdAsync(Guid provinceId);
	Task<List<District>> SearchDistrictsAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1);
	Task<District> UpdateDistrictAsync(District district);
	Task<int> DeleteDistrictAsync(Guid id);
}
