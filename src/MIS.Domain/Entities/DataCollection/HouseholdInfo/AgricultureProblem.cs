using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class AgricultureProblem : BaseEntity
{
    public Guid AgricultureId { get; set; }
    public Guid ProblemId { get; set; }
    public string? Details { get; set; } // optional extra detail

    public Agriculture Agriculture { get; set; } = null!;
    public OptionItem Problem { get; set; } = null!;
}