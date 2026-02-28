using Microsoft.EntityFrameworkCore;
using Mis.API.Models;

namespace MIS.API.Dtos;

public class AppUserDTO
{

    public class RegisterDto
    {
        public string userName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone {get; set;} = null!;
        public string Password { get; set; } = null!;

    }

    public class LoginDto
    {
        public string UserName {get; set;} = null!;
        public string Password {get; set;} = null!;
    }

    public class AuthResponseDto
    {
        public string Token {get; set;} = null!;
        public DateTime ExpiresAt {get; set;}
    }
}