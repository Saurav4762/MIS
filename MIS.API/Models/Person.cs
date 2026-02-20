namespace MIS.API.Models
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public Guid HouseholdId { get; set; }
        public int RowNo { get; set; }
        public string? WardNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string? HhId { get; set; }
        public int? FamilymemberNum { get; set; }
        public string? MemberName { get; set; }
        public decimal? Age { get; set; }
        public string? PersonalContact { get; set; }
        public string? KnowPersonalContact { get; set; }
        public string? MemberPhNum { get; set; }
        public string? FatherName { get; set; }
        public string? Relationships { get; set; }
        public string? RelationshipsOtherText { get; set; }
        public string? Gender { get; set; }
        public string? TestBloodGroup { get; set; }
        public string? BloodGroup { get; set; }
        public string? HasEmailId { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? Childbornplace { get; set; }
        public string? ChildbornplaceOtherText { get; set; }
        public string? Fullmilkcourse { get; set; }
        public int? Durationbrestfeed { get; set; }
        public string? Childhealthcheckup { get; set; }
        public string? Childbmi { get; set; }
        public string? Vitamina { get; set; }
        public string? Schooladmission { get; set; }
        public string? SchooladmissionOtherText { get; set; }
        public decimal? Childedugrade { get; set; }
        public string? Childphydev { get; set; }
        public string? ChildphydevOtherText { get; set; }
        public string? Childlgdev { get; set; }
        public string? ChildlgdevOtherText { get; set; }
        public string? Childsocialization { get; set; }
        public string? ChildsocializationOtherText { get; set; }
        public string? Goingschool { get; set; }
        public int? Schoolgradesixtoten { get; set; }
        public string? Schooltype { get; set; }
        public string? ScholtypeOtherText { get; set; }
        public string? Goingschooltentofifteen { get; set; }
        public int? Schoolgradetentofifteen { get; set; }
        public string? Schoolkind { get; set; }
        public string? SchoolkindOtherText { get; set; }
        public string? Schoolingstatus { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
        public ICollection<PersonChildvaccine> ChildvaccineLinks { get; set; } = new List<PersonChildvaccine>();
        public ICollection<PersonCausenotgoingschool> CausenotgoingschoolLinks { get; set; } = new List<PersonCausenotgoingschool>();
    }
}
