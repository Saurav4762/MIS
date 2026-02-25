namespace MIS.API.Models;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string BloodGroup { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BirthPlace { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } 
}