using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class Household
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public Guid SubmissionId { get; set; }
        public int? WardNo { get; set; }
        public string? Area { get; set; }
        public string? AreaTole { get; set; }
        public string? Tolename { get; set; }
        public string? Tole { get; set; }
        public string? HouseNo { get; set; }
        public string? Serialno { get; set; }
        public int? HhNo { get; set; }
        public string? HhId { get; set; }
        public string? Location { get; set; }
        public string? HouseOwnership { get; set; }
        public string? HouseOwnershipOtherText { get; set; }
        public string? HouseInfoTaken { get; set; }
        public string? ImageHousemaster { get; set; }
        public string? ImageHouse { get; set; }
        public string? Sameperson { get; set; }
        public string? Nameinformant { get; set; }
        public string? DatacollectionIssues { get; set; }
        public string? AreaCode { get; set; }
        public string? ToleCode { get; set; }
        public string? ToleNameLocal { get; set; }
        public string? Geom { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Submission? Submission { get; set; }
        public HhHouseInfo? HouseInfo { get; set; }
        public HhFamilyInfo? FamilyInfo { get; set; }
        public HhSocioCultural? SocioCultural { get; set; }
        public HhSanitationandhygine? Sanitationandhygine { get; set; }
        public HhFamilydecision? Familydecision { get; set; }
        public HhFamilyFacilities? FamilyFacilities { get; set; }
        public HhTransportFacility? TransportFacility { get; set; }
        public HhAgricultureInformation? AgricultureInformation { get; set; }
        public HhProdFoodcrops? ProdFoodcrops { get; set; }
        public HhProdPulsecrops? ProdPulsecrops { get; set; }
        public HhProdOilcrops? ProdOilcrops { get; set; }
        public HhVegetables? Vegetables { get; set; }
        public HhProdCashcrops? ProdCashcrops { get; set; }
        public HhProdFriuts? ProdFriuts { get; set; }
        public HhProdLivestock? ProdLivestock { get; set; }
        public HhAiLivestock? AiLivestock { get; set; }
        public HhProdBeeFishSilk? ProdBeeFishSilk { get; set; }
        public HhAgrovetProduction? AgrovetProduction { get; set; }
        public HhFamilyIncome? FamilyIncome { get; set; }
        public HhFamilyExpense? FamilyExpense { get; set; }
        public HhDisasterInfo? DisasterInfo { get; set; }
        public HhVictimhfromnd? Victimhfromnd { get; set; }
        public HhVictimphyfromnd? Victimphyfromnd { get; set; }
        public ICollection<Person> Persons { get; set; } = new List<Person>();
        public ICollection<HhAicowdetail> AicowDetails { get; set; } = new List<HhAicowdetail>();
        public ICollection<HhAibuffalodetail> AibuffaloDetails { get; set; } = new List<HhAibuffalodetail>();
        public ICollection<HhAigoatdetail> AigoatDetails { get; set; } = new List<HhAigoatdetail>();
        public ICollection<HhAiswinedetail> AiswineDetails { get; set; } = new List<HhAiswinedetail>();
        public ICollection<HouseholdSourceirrigation> SourceirrigationLinks { get; set; } = new List<HouseholdSourceirrigation>();
        public ICollection<HouseholdLoansource> LoansourceLinks { get; set; } = new List<HouseholdLoansource>();
        public ICollection<HouseholdLoanpurpose> LoanpurposeLinks { get; set; } = new List<HouseholdLoanpurpose>();
        public ICollection<HouseholdParticipationinorg> ParticipationinorgLinks { get; set; } = new List<HouseholdParticipationinorg>();
        public ICollection<HouseholdAlterSourceOfLight> AlterSourceOfLightLinks { get; set; } = new List<HouseholdAlterSourceOfLight>();
        public ICollection<HouseholdMunIssues> MunIssuesLinks { get; set; } = new List<HouseholdMunIssues>();
    }
}
