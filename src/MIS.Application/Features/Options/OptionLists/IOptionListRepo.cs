
namespace MIS.Application.Features.Options.OptionLists;


public interface IOptionListRepo
{
  public Task<OptionList> CreateOptionListAsync(OptionList optionList);
  public Task<List<OptionList>> GetAllOptionListsAsync();
  public Task<OptionList> GetOptionListByIdAsync(Guid id);
  public Task<OptionList> Update(OptionList optionList);
  public Task<int> ExecuteDeleteAsync(Guid id);
}