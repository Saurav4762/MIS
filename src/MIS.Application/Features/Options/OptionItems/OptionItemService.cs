
using FluentValidation;
using MIS.Application.Common.Validations;
using MIS.Application.Features.Options.OptionLists;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Options.OptionItems;


public class OptionItemService : IOptionItemService
{
  private readonly IOptionItemRepo _repo;
  private readonly IOptionListRepo _optionListRepo;
  private readonly IValidator<CreateOptionItemDTO> _createOptionItemValidator;
  public OptionItemService(IOptionItemRepo repo, IOptionListRepo optionListRepo, IValidator<CreateOptionItemDTO> createOptionItemValidator)
  {
    _repo = repo;
    _optionListRepo = optionListRepo;
    _createOptionItemValidator = createOptionItemValidator;
  }

  public async Task<OptionItem> CreateOptionItem(CreateOptionItemDTO dto)
  {
    await _createOptionItemValidator.EnsureValidOrThrowAsync(dto);
    
    return await _repo.CreateOptionItemAsync(
      new OptionItem
      {
        Id = Guid.NewGuid(),
        OptionListId = dto.OptionListId,
        Extra = dto.Extra,
        LabelEn = dto.LabelEn,
        LabelNe = dto.LabelNe
      });
  }

  public async Task DeleteOptionItem(Guid id)
  {
    if (await _repo.GetOptionItemByIdAsync(id) is null)
    {
      throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);
    }

    await _repo.ExecuteDeleteAsync(id);
  }

  public async Task<List<OptionItem>> GetOptionItemsByOptionListId(Guid optionListId)
  {
    var optionList = await _optionListRepo.GetOptionListByIdAsync(optionListId) ??
      throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), optionListId);
    return await _repo.GetOptionItemByOptionListIdAsync(optionListId);
  }

  public async Task<OptionItem> UpdateOptionItem(Guid id, UpdateOptionItemDTO dto)
  {
    var optionItem = await _repo.GetOptionItemByIdAsync(id) ??
    throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);


    if (!string.IsNullOrEmpty(dto.LabelEn))
    {
      optionItem.LabelEn = dto.LabelEn;
    }
    if (!string.IsNullOrEmpty(dto.LabelNe))
    {
      optionItem.LabelNe = dto.LabelNe;
    }
    if (dto.Extra is not null)
    {
      optionItem.Extra = dto.Extra;
    }

    await _repo.UpdateOptionItemAsync(optionItem);


    return optionItem;
  }
}