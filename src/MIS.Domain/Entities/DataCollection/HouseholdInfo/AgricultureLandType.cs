using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class AgricultureLandType : BaseEntity
{
    public Guid AgricultureId { get; set; }
    public Guid LandTypeId { get; set; }

    // Optional:
    public decimal? Area { get; set; }

    public Agriculture Agriculture { get; set; } = null!;
    public OptionItem LandType { get; set; } = null!;
}
