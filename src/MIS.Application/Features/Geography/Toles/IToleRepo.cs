using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Toles;

public interface IToleRepo
{
	Task<Tole> CreateToleAsync(Tole tole);
	Task<List<Tole>> GetAllTolesAsync();
	Task<List<Tole>> GetTolesByWardIdAsync(Guid wardId);
	Task<Tole?> GetToleByIdAsync(Guid id);
	Task<Tole> UpdateToleAsync(Tole tole);
	Task<int> DeleteToleAsync(Guid id);
}
