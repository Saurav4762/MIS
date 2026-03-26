
namespace MIS.Application.Features.Options.OptionLists;


public interface IOptionListService
{
  public Task<OptionList> CreateOptionListAsync(CreateOptionListDTO dto);
  public Task<List<OptionList>> GetAllOptionListsAsync();
  public Task<OptionList> UpdateOptionListAsync(Guid id, UpdateOptionListDTO dto);
  public Task DeleteOptionListByIdAsync(Guid id);
}