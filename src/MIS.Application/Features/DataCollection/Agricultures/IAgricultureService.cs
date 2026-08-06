namespace MIS.Application.Features.DataCollection.Agricultures;
public interface IAgricultureService
{
    Task<AgricultureDTO> CreateAgricultureAsync(CreateAgricultureDTO dto);
    Task<AgricultureDTO> UpdateAgricultureAsync(Guid id,UpdateAgricultureDTO dto);
    Task<AgricultureDTO> GetAgricultureByIdAsync(Guid id);
    Task<List<AgricultureDTO>> GetAllAgricultureAsync();
    Task DeleteAgricultureAsync(Guid id);
}