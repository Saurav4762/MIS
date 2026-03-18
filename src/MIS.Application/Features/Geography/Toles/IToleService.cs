using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Toles;

public interface IToleService
{
	Task<Tole> CreateToleAsync(CreateToleDTO dto);
	Task<List<Tole>> GetAllTolesAsync();
	Task<List<Tole>> GetTolesByWardIdAsync(Guid wardId);
	Task<Tole> GetToleByIdAsync(Guid id);
	Task<Tole> UpdateToleAsync(Guid id, UpdateToleDTO dto);
	Task DeleteToleAsync(Guid id);
}
