namespace MIS.Application.Features.Geography.Provinces;

public interface IProvinceService
{
	Task<ProvinceDTO> CreateProvinceAsync(CreateProvinceDTO province);
	Task<List<ProvinceDTO>> GetAllProvincesAsync();
	Task<ProvinceDTO> GetProvinceByIdAsync(Guid id);
	Task<ProvinceDTO> UpdateProvinceAsync(Guid id, UpdateProvinceDTO province);
	Task DeleteProvinceAsync(Guid id);
	Task<List<ProvinceDTO>> SearchProvincesAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1);
}
