using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;


public class EconomyService(IEconomyRepo economyRepo, IOptionItemRepo optionItemRepo, IValidator<CreateEconomyDto> validator) : IEconomyService
{
  private readonly IEconomyRepo _economyRepo = economyRepo;
  private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;
  private readonly IValidator<CreateEconomyDto> _validator = validator;

  public decimal CalculateTotalAnnualExpenditure(EconomyDto economyDto)
  {
    return economyDto.FoodExpenditure +
           economyDto.EducationExpenditure +
           economyDto.HealthExpenditure +
           economyDto.ClothingExpenditure +
           economyDto.AgricultureExpenditure +
           economyDto.OtherExpenditure;
  }

  public async Task<EconomyDto> CreateEconomyAsync(CreateEconomyDto createEconomyDto)
  {
    await _validator.EnsureValidOrThrowAsync(createEconomyDto);
    // check if classification status exists
    var classificationStatus = await _optionItemRepo.CheckIfOptionItemExistsAsync(createEconomyDto.ClassificationStatusId);
    if (!classificationStatus)
    {
      throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createEconomyDto.ClassificationStatusId);
    }

    var mainIncomeSource = await _optionItemRepo.CheckIfOptionItemExistsAsync(createEconomyDto.MainIncomeSourceId);
    if (!mainIncomeSource)
    {
      throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createEconomyDto.MainIncomeSourceId);
    }

    if (createEconomyDto.LoanSourceId.HasValue)
    {
      var loanSource = await _optionItemRepo.CheckIfOptionItemExistsAsync(createEconomyDto.LoanSourceId.Value);
      if (!loanSource)
        throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), createEconomyDto.LoanSourceId.Value);
    }


    var economy = new Economy
    {
      FamilyId = createEconomyDto.FamilyId,
      ClassificationStatusId = createEconomyDto.ClassificationStatusId,
      MainIncomeSourceId = createEconomyDto.MainIncomeSourceId,
      LoanSourceId = createEconomyDto.LoanSourceId,

      HasFinancialLoan = createEconomyDto.HasFinancialLoan,
      FoodExpenditure = createEconomyDto.FoodExpenditure,
      EducationExpenditure = createEconomyDto.EducationExpenditure,
      HealthExpenditure = createEconomyDto.HealthExpenditure,
      ClothingExpenditure = createEconomyDto.ClothingExpenditure,
      AgricultureExpenditure = createEconomyDto.AgricultureExpenditure,
      OtherExpenditure = createEconomyDto.OtherExpenditure
    };


    var newEconomy = await _economyRepo.CreateEconomyAsync(economy);
    return newEconomy.ToEconomyDto();
  }

  public async Task<EconomyDto> GetEconomyByFamilyIdAsync(Guid familyId)
  {
    var economy = await _economyRepo.GetEconomyByFamilyIdAsync(familyId);
    return economy?.ToEconomyDto() ?? throw new NotFoundException(nameof(Family), nameof(Family.Id), familyId);
  }

  public async Task<EconomyDto> UpdateEconomyAsync(Guid familyId, EconomyDto economyDto)
  {
    throw new NotImplementedException();
  }
}
