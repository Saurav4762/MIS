
using FluentValidation;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Options.OptionLists;


public class OptionListService : IOptionListService
{
  private readonly IOptionListRepo _repo;
  private readonly IValidator<CreateOptionListDTO> _createOptionListValidator;
  public OptionListService(
      IOptionListRepo repo,
      IValidator<CreateOptionListDTO> createOptionListValidator
    )
  {
    _repo = repo;
    _createOptionListValidator = createOptionListValidator;
  }

  public async Task<OptionList> CreateOptionListAsync(CreateOptionListDTO dto)
  {
    var result = await _createOptionListValidator.ValidateAsync(dto);

    if (!result.IsValid)
    {
      throw new DataValidationException(
        result.Errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            x => x.Key,
            x => x.Select(x => x.ErrorMessage).ToArray()
          )
      );
    }

    return await _repo.CreateOptionListAsync(new OptionList
    {
      Id = Guid.NewGuid(),
      Description = dto.Description,
      LabelEn = dto.LabelEn,
      LabelNe = dto.LabelNe
    });
  }
  public async Task<List<OptionList>> GetAllOptionListsAsync()
  {
    return await _repo.GetAllOptionListsAsync();
  }

  public async Task<OptionList> UpdateOptionListAsync(Guid id, UpdateOptionListDTO dto)
  {
    var optionList = await _repo.GetOptionListByIdAsync(id) ??
      throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), id);

    if (!string.IsNullOrEmpty(dto.LabelEn))
    {
      optionList.LabelEn = dto.LabelEn;
    }
    if (!string.IsNullOrEmpty(dto.LabelNe))
    {
      optionList.LabelNe = dto.LabelNe;
    }
    if (!string.IsNullOrEmpty(dto.Description))
    {
      optionList.Description = dto.Description;
    }

    return await _repo.Update(optionList);
  }

  public async Task DeleteOptionListByIdAsync(Guid id)
  {
    var optionList = await _repo.GetOptionListByIdAsync(id) ??
      throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), id);

    await _repo.ExecuteDeleteAsync(id);
  }

}