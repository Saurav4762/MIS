namespace MIS.API.DTOs;

public class PersonDTO
{
    public class CreatePersonDto
    {
        public string FirstName { get; set; } = string.Empty;
        
        public string MiddleName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        public string FatherName {get; set; } = string.Empty;
        
        public string Gender { get; set; } = string.Empty;
        
        public string BloodGroup {get; set; } = string.Empty;
        
        public string Email {get; set; } = string.Empty;
        
        public string BirthPlace { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; }
    }

    public class PersonResponseDto
    {
        public Guid Id { get; set; }
        
        public string FullName {get; set; } = string.Empty;
        
        public string Gender {get; set; } = string.Empty;
        public string Email {get; set; } = string.Empty;
        
        public DateTime DateOfBirth {get; set; }
    }
}