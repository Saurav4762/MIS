namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;


public class DecisionDTO
{
  public Guid FamilyId { get; set; }
  public Guid? HouseholdExpenseId { get; set; }
  public Guid? PropertyId { get; set; }
  public Guid? HealthCareId { get; set; }
  public Guid? EducationId { get; set; }
  public Guid? InvestmentsId { get; set; }
  public Guid? GovernanceId { get; set; }

}