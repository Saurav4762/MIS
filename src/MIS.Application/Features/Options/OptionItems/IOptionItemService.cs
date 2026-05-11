namespace MIS.Application.Features.Options.OptionItems;

public interface IOptionItemService
{
  public Task<OptionItemDTO> CreateOptionItem(CreateOptionItemDTO dto);
  public Task<List<OptionItemDTO>> GetOptionItemsByOptionListId(Guid Id);

  public Task<OptionItemDTO> UpdateOptionItem(Guid id, UpdateOptionItemDTO dto);

  public Task DeleteOptionItem(Guid id);
}