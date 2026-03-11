using Microsoft.EntityFrameworkCore;
using Mis.API.Models;

namespace MIS.API.DTOs;

public class AuthDTOs
{
    public class LoginRequestDto
    {
        public string UserName {get; set;} = null!;
        
        public string Password {get; set;} = null!;
    }

    public class LoginResponseDto
    {
        public string Token {get; set;} = null!;
        
        public string Username {get; set;} = null!;
        
        public string FullName {get; set;} = null!;
        
        public DateTime ExpiresAt {get; set;}
        
        public List<string> Roles {get; set;} = null!;
    }
}