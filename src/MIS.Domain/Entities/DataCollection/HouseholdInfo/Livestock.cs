using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Livestock : BaseEntity
{
    // Foreign Key linking to the main Family unit
    public Guid FamilyId { get; set; }

    // Section Toggles
    public bool HasLivestockPractice { get; set; }   // Section 01 Header Toggle
    public bool HasAIServicePractice { get; set; }   // Section 02 Header Toggle

    // Navigation Property back to Family
    public Family Family { get; set; } = null!;

    // 1-to-Many Collections pointing to the child tables
    public ICollection<LivestockAnimal> Animals { get; set; } = new List<LivestockAnimal>();
    public ICollection<LivestockAIService> AIServices { get; set; } = new List<LivestockAIService>();
}