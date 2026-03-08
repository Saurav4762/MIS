namespace MIS.API.DTOs;

public class UserDTOs
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = null!;
        
        public string FullName {get; set; } = null!;
        
        public string Phone {get; set; } = null!;
        
        public string Email {get; set; } = null!;
        
        public string Password {get; set; } = null!;
    }

    public class UpdateUserRequest
    {
        public string FullName {get; set; } = null!;
        
        public string Phone {get; set; } = null!;
        
        public string Email {get; set; } = null!;
        
        public bool IsActive {get; set; }
    }

    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; } = null!;
        
        public string FullName { get; set; } = null!;
        
        public string Phone { get; set; } = null!;
        
        public string Email { get; set; } = null!;
        
        public bool IsActive { get; set; }
        
        public DateTime Created { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}