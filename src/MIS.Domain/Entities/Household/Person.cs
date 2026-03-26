using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.Educations;

namespace MIS.Domain.Entities.HouseHold;
public class Person : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string BloodGroup { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BirthPlace { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    // Navigation Property
    public IEnumerable<Education>? Educations { get; set; } = [];
    public Family? Family { get; set; } = null;

}