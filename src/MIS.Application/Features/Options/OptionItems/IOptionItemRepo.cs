namespace MIS.Application.Features.Options.OptionItems;

public interface IOptionItemRepo
{
  public Task<OptionItem> CreateOptionItemAsync(OptionItem dto);
  public Task<List<OptionItem>> GetOptionItemByOptionListIdAsync(Guid opitonListId);

  public Task<OptionItem?> GetOptionItemByIdAsync(Guid id);
  public Task<OptionItem> UpdateOptionItemAsync(OptionItem optionItem);
  public Task ExecuteDeleteAsync(Guid id);
}