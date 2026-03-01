namespace MIS.API.DTOs;

public class AppRoleDTO
{
     public class CreateRoleDto
     {
          public string RoleCode { get; set; } = string.Empty;
          public string RoleName { get; set; } = string.Empty;
     }

     public class RoleResponseDto
     {
          public Guid Id { get; set; }
          public string RoleCode { get; set; } = string.Empty;
          public string RoleName { get; set; } = string.Empty;
     }
}