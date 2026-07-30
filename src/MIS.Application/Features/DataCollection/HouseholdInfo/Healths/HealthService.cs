using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Common.Interfaces;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public class HealthService(
    IHealthRepo healthRepo,
    IOptionItemRepo optionItemRepo,
    IValidator<CreateHealthDto> createValidator,
    IValidator<UpdateHealthDto> updateValidator,
    IUnitOfWork unitOfWork) : IHealthService
{
  private readonly IHealthRepo _healthRepo = healthRepo;
  private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;
  private readonly IValidator<CreateHealthDto> _createValidator = createValidator;
  private readonly IValidator<UpdateHealthDto> _updateValidator = updateValidator;
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<HealthDto> GetHealthByFamilyIdAsync(Guid familyId)
  {
    var health = await _healthRepo.GetHealthByFamilyIdAsync(familyId);
    return health?.ToHealthDto()
        ?? throw new NotFoundException(nameof(Family), nameof(Health.FamilyId), familyId);
  }

  public async Task<HealthDto> CreateHealthAsync(CreateHealthDto createHealthDto)
  {
    await _createValidator.EnsureValidOrThrowAsync(createHealthDto);

    var existingHealth = await _healthRepo.GetHealthByFamilyIdAsync(createHealthDto.FamilyId);
    if (existingHealth is not null)
      throw new DataValidationException(nameof(Health), $"Health already exists with family id: {existingHealth.Id}");

    // Validate optional InsuranceProvider FK
    if (createHealthDto.HasHealthInsurance && !createHealthDto.InsuranceProviderId.HasValue)
      throw new DataValidationException(nameof(Health.HasHealthInsurance), $"Insurance provider must be provided if hasHealthInsurance is true");

    if (createHealthDto.HasHealthInsurance && createHealthDto.InsuranceProviderId.HasValue)
      await ValidateOptionExistsAsync(createHealthDto.InsuranceProviderId.Value);

    if (createHealthDto.HasChronicIllness && createHealthDto.ChronicIllnessIds.Count.Equals(0))
      throw new DataValidationException(nameof(Health.HasChronicIllness), "At least one ChronicIllnessId is required when HasChronicIllness is true.");

    // Validate every chronic illness FK
    // if (createHealthDto.HasChronicIllness)
    //   foreach (var illnessId in createHealthDto.ChronicIllnessIds)
    //     await ValidateOptionExistsAsync(illnessId);

    // Fetch OptionItem entities for the many-to-many join
    var chronicIllnessItems = new List<OptionItem>();
    if (createHealthDto.HasChronicIllness && createHealthDto.ChronicIllnessIds.Count > 0)
    {
      foreach (var id in createHealthDto.ChronicIllnessIds)
      {
        var item = await _optionItemRepo.GetOptionItemByIdAsync(id)
            ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);
        chronicIllnessItems.Add(item);
      }
    }

    var health = new Health
    {
      FamilyId = createHealthDto.FamilyId,
      HasHandwashingPractice = createHealthDto.HasHandwashingPractice,
      HasNutritionAwareness = createHealthDto.HasNutritionAwareness,
      HasPregnancyCareAwareness = createHealthDto.HasPregnancyCareAwareness,
      HasUnder5Checkups = createHealthDto.HasUnder5Checkups,
      HasIronFolicSupplements = createHealthDto.HasIronFolicSupplements,
      HasCompleteVaccination = createHealthDto.HasCompleteVaccination,
      HasHealthInsurance = createHealthDto.HasHealthInsurance,
      InsuranceProviderId = createHealthDto.HasHealthInsurance ? createHealthDto.InsuranceProviderId : null,
      HasChronicIllness = createHealthDto.HasChronicIllness,
      ChronicIllnesses = chronicIllnessItems
    };

    var created = await _healthRepo.CreateHealthAsync(health);
    return created.ToHealthDto();
  }

  public async Task<HealthDto> UpdateHealthAsync(Guid familyId, UpdateHealthDto updateHealthDto)
  {
    await _updateValidator.EnsureValidOrThrowAsync(updateHealthDto);

    var health = await _healthRepo.GetHealthByFamilyIdAsync(familyId)
        ?? throw new NotFoundException(nameof(Health), nameof(Health.FamilyId), familyId);

    // Section 01: Awareness — patch only provided fields
    if (updateHealthDto.HasHandwashingPractice.HasValue)
      health.HasHandwashingPractice = updateHealthDto.HasHandwashingPractice.Value;
    if (updateHealthDto.HasNutritionAwareness.HasValue)
      health.HasNutritionAwareness = updateHealthDto.HasNutritionAwareness.Value;
    if (updateHealthDto.HasPregnancyCareAwareness.HasValue)
      health.HasPregnancyCareAwareness = updateHealthDto.HasPregnancyCareAwareness.Value;
    if (updateHealthDto.HasUnder5Checkups.HasValue)
      health.HasUnder5Checkups = updateHealthDto.HasUnder5Checkups.Value;
    if (updateHealthDto.HasIronFolicSupplements.HasValue)
      health.HasIronFolicSupplements = updateHealthDto.HasIronFolicSupplements.Value;
    if (updateHealthDto.HasCompleteVaccination.HasValue)
      health.HasCompleteVaccination = updateHealthDto.HasCompleteVaccination.Value;

    // Section 02: Health Insurance
    if (updateHealthDto.HasHealthInsurance.HasValue)
    {
      health.HasHealthInsurance = updateHealthDto.HasHealthInsurance.Value;

      if (health.HasHealthInsurance && updateHealthDto.InsuranceProviderId.HasValue)
      {
        await ValidateOptionExistsAsync(updateHealthDto.InsuranceProviderId.Value);
        health.InsuranceProviderId = updateHealthDto.InsuranceProviderId;
      }
      else if (health.HasHealthInsurance && !updateHealthDto.InsuranceProviderId.HasValue)
      {
        throw new DataValidationException(nameof(health.HasHealthInsurance), "InsuranceProviderId is required when HasHealthInsurance is true.");
      }
      else if (!health.HasHealthInsurance)
      {
        health.InsuranceProviderId = null;
      }
    }

    // Section 03: Chronic Illness
    if (updateHealthDto.HasChronicIllness.HasValue)
      health.HasChronicIllness = updateHealthDto.HasChronicIllness.Value;



    if (updateHealthDto.HasChronicIllness.HasValue && updateHealthDto.HasChronicIllness.Value && (updateHealthDto.ChronicIllnessIds is null || updateHealthDto.ChronicIllnessIds.Count == 0))
    {
      throw new DataValidationException(nameof(health), "At least one ChronicIllnessId is required when HasChronicIllness is true.");
    }


    if (updateHealthDto.HasChronicIllness.HasValue && updateHealthDto.HasChronicIllness.Value && updateHealthDto.ChronicIllnessIds is not null && updateHealthDto.ChronicIllnessIds.Count != 0)
    {
      health.ChronicIllnesses.Clear();
      foreach (var id in updateHealthDto.ChronicIllnessIds)
      {
        var item = await _optionItemRepo.GetOptionItemByIdAsync(id)
            ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);
        health.ChronicIllnesses.Add(item);
      }

    }
    else if (updateHealthDto.HasChronicIllness.HasValue && !updateHealthDto.HasChronicIllness.Value)
    {
      health.ChronicIllnesses.Clear();
    }
    
    await _unitOfWork.SaveChangesAsync();
    return health.ToHealthDto();
  }

  private async Task ValidateOptionExistsAsync(Guid optionId)
  {
    var exists = await _optionItemRepo.CheckIfOptionItemExistsAsync(optionId);
    if (!exists)
      throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), optionId);
  }
}
