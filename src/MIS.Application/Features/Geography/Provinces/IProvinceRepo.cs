using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Provinces;

public interface IProvinceRepo
{
	Task<Province> CreateProvinceAsync(Province province);
	Task<List<Province>> GetAllProvincesAsync();
	Task<Province?> GetProvinceByIdAsync(Guid id);
	Task<Province?> GetByCodeAsync(string code);
	Task<List<Province>> SearchProvincesAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1);
	Task<Province> UpdateProvinceAsync(Province province);
	Task<int> DeleteProvinceAsync(Guid id);
}
