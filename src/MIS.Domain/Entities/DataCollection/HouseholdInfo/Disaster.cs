using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Disaster : BaseEntity
{
     public Guid FamilyId { get; set; }
     public Guid? DisasterTypeId { get; set; } // Nullable dropdown choice for past disaster type
    
     // Section 01: Threat Assessment (Hazards Checklist)
     public bool IsFloodProne { get; set; }
     public bool IsEarthquakeProne { get; set; }
     public bool IsFireProne { get; set; }
     public bool IsLandslideProne { get; set; }
     public bool IsWindstormProne { get; set; }
     public bool IsEpidemicProne { get; set; }
     
     // Section 02: Disaster History
     public bool IsVictimPast3To5Years { get; set; }
     public int? DisasterYearBs { get; set; }
     public string? DisasterDescription { get; set; }
     public bool HasDeathInjuryPastYear { get; set; }
     public int NumberOfDeaths { get; set; }

     // Section 03: Preparedness
     public bool HasHouseholdPlan { get; set; }
     public bool HasEarlyWarningRouteInfo { get; set; }
     public bool HasEmergencyKit { get; set; }
     public bool HasTrainingParticipation { get; set; }

     // Navigation property
     public Family Family { get; set; } = null!;
     public OptionItem? DisasterType { get; set; }

}