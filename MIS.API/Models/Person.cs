using System.ComponentModel.DataAnnotations;
using MIS.API.Enum;

namespace MIS.API.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public Gender Gender { get; set; } 
    public string BloodGroup { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BirthPlace { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    // Navigation Property
    public IEnumerable<HouseOwner>? HouseOwners { get; set; } = null;
    public IEnumerable<Education>? Educations { get; set; } = [];
    public Family? Family { get; set; } = null;

}