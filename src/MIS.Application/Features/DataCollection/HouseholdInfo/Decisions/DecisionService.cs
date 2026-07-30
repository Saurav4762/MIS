using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public class DecisionService(IDecisionRepo decisionRepo, IOptionItemRepo optionItemRepo, IValidator<CreateDecisionDTO> validator) : IDecisionService
{
  private readonly IDecisionRepo _repo = decisionRepo;
  private readonly IOptionItemRepo _optionItemService = optionItemRepo;
  private readonly IValidator<CreateDecisionDTO> _validator = validator;

  public async Task<DecisionDTO> CreateDecision(CreateDecisionDTO createDecisionDTO)
  {
    await _validator.EnsureValidOrThrowAsync(createDecisionDTO);

    if (createDecisionDTO.HouseholdExpenseId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.HouseholdExpenseId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.HouseholdExpenseId.Value);
    }

    if (createDecisionDTO.PropertyId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.PropertyId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.PropertyId.Value);
    }

    if (createDecisionDTO.HealthCareId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.HealthCareId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.HealthCareId.Value);
    }

    if (createDecisionDTO.EducationId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.EducationId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.EducationId.Value);
    }

    if (createDecisionDTO.InvestmentsId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.InvestmentsId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.InvestmentsId.Value);
    }
    if (createDecisionDTO.GovernanceId.HasValue)
    {
      var exists = await _optionItemService.CheckIfOptionItemExistsAsync(createDecisionDTO.GovernanceId.Value);
      if (!exists)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createDecisionDTO.GovernanceId.Value);
    }


    var decision = new Decision
    {
      FamilyId = createDecisionDTO.FamilyId,
      HouseholdExpenseId = createDecisionDTO.HouseholdExpenseId,
      EducationId = createDecisionDTO.EducationId,
      GovernanceId = createDecisionDTO.GovernanceId,
      HealthCareId = createDecisionDTO.HealthCareId,
      InvestmentsId = createDecisionDTO.InvestmentsId,
      PropertyId = createDecisionDTO.PropertyId,
    };
    return (await _repo.CreateDecision(decision)).MapToDecisionDTO();
  }

  public async Task<DecisionDTO> GetDecisionByFamilyId(Guid familyId)
  {
    var decision = await _repo.GetDecisionByFamilyId(familyId);
    return decision?.MapToDecisionDTO() ?? throw new NotFoundException(nameof(Family), nameof(Decision.FamilyId), familyId);
  }

  public async Task<DecisionDTO> UpdateDecision(Guid familyId, UpdateDecisionDTO updateDecisionDTO)
  {
    throw new NotImplementedException();
  }
}