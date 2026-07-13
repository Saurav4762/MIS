using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Toles;

public interface IToleService
{
	Task<ToleDTO> CreateToleAsync(CreateToleDTO dto);
	Task<List<ToleDTO>> GetAllTolesAsync();
	Task<List<ToleDTO>> GetTolesByWardIdAsync(Guid wardId);
	Task<ToleDTO> GetToleByIdAsync(Guid id);
	Task<ToleDTO> UpdateToleAsync(Guid id, UpdateToleDTO dto);
	Task DeleteToleAsync(Guid id);
}
