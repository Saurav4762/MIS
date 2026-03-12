using MIS.API.DTOs;
using static MIS.API.DTOs.WardDTO;

namespace MIS.API.Interfaces.IServices;

public interface IWardService
{
    Task<IEnumerable<WardResponse>> GetAllAsync();

    Task<WardResponse> GetByIdAsync(Guid id);

    Task<WardResponse> CreateAsync(WardRequest dto);

    Task<WardResponse> UpdateAsync(Guid id, WardRequest dto);

    Task DeleteAsync(Guid id);
}