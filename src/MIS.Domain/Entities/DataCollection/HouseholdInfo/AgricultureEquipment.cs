using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class AgricultureEquipment : BaseEntity
{
    public Guid AgricultureId { get; set; }
    public Guid EquipmentId { get; set; }   // OptionItem id

    // Additional fields:
    public int Quantity { get; set; }
    public string? Condition { get; set; }  // e.g., Good, Fair, Poor

    public Agriculture Agriculture { get; set; } = null!;
    public OptionItem Equipment { get; set; } = null!;
}