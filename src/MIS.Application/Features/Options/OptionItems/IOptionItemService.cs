namespace MIS.Application.Features.Options.OptionItems;

public interface IOptionItemService
{
  public Task<OptionItem> CreateOptionItem(CreateOptionItemDTO dto);
  public Task<List<OptionItem>> GetOptionItemsByOptionListId(Guid Id);

  public Task<OptionItem> UpdateOptionItem(Guid id, UpdateOptionItemDTO dto);

  public Task DeleteOptionItem(Guid id);
}