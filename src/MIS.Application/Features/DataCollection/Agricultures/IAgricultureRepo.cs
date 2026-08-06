using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.Agricultures;

public interface IAgricultureRepo
{
    public Task<Agriculture> CreateAgricultureAsync(Agriculture agriculture);
    public Task<List<Agriculture>> GetAllAgriculturesAsync();
    public Task<Agriculture?> GetAgricultureByIdAsync(Guid id);
    public Task<Agriculture> UpdateAgricultureAsync(Agriculture agriculture);
    public Task DeleteAgricultureAsync(Guid id);
}


