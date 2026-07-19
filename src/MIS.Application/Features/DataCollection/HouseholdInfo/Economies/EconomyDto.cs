
// Economy Dto class for data transfer between layers
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;

public class EconomyDto
{
    // Foreign Keys
    public Guid FamilyId { get; set; }
    public Guid ClassificationStatusId { get; set; } // Rich, Medium, Poor, Very Poor
    public Guid MainIncomeSourceId { get; set; }      // Agriculture, Employment, Business, etc.
    public Guid? LoanSourceId { get; set; }           // Nullable: Only active if HasFinancialLoan is true

    // Section 01: Status & Sources
    public bool HasFinancialLoan { get; set; }

    // Section 02: Annual Expenditure (NPR)
    public decimal FoodExpenditure { get; set; }
    public decimal EducationExpenditure { get; set; }
    public decimal HealthExpenditure { get; set; }
    public decimal ClothingExpenditure { get; set; }
    public decimal AgricultureExpenditure { get; set; }
    public decimal OtherExpenditure { get; set; }

    // Domain Calculated Property (Does not require database storage)
    public decimal TotalAnnualExpenditure => 
        FoodExpenditure + 
        EducationExpenditure + 
        HealthExpenditure + 
        ClothingExpenditure + 
        AgricultureExpenditure + 
        OtherExpenditure;
}