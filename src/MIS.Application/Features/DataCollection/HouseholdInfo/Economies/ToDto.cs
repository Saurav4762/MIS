using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;

public static class ToDto
{
  public static EconomyDto ToEconomyDto(this Economy economy)
  {
    return new EconomyDto
    {
      FamilyId = economy.FamilyId,
      ClassificationStatusId = economy.ClassificationStatusId,
      MainIncomeSourceId = economy.MainIncomeSourceId,
      LoanSourceId = economy.LoanSourceId,
      HasFinancialLoan = economy.HasFinancialLoan,
      FoodExpenditure = economy.FoodExpenditure,
      EducationExpenditure = economy.EducationExpenditure,
      HealthExpenditure = economy.HealthExpenditure,
      ClothingExpenditure = economy.ClothingExpenditure,
      AgricultureExpenditure = economy.AgricultureExpenditure,
      OtherExpenditure = economy.OtherExpenditure
    };
  }
}
