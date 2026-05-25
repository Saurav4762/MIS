using System.Security.Claims;

namespace MIS.API.Common;

public class CurrentUser
{
    public Guid UserId { get; }
    public string Email { get; }
    public string Role { get; }
    public Guid? MunicipalityId { get; }
    public bool IsSuperAdmin => Role == "SuperAdmin" || 
                                string.IsNullOrEmpty(MunicipalityId?.ToString());

    public CurrentUser(ClaimsPrincipal principal)
    {
        UserId = Guid.Parse(
            principal.FindFirstValue("sub") ?? Guid.Empty.ToString());

        Email = principal.FindFirstValue("email") ?? "";

        // Try all possible claim type formats
        Role = principal.FindFirstValue(ClaimTypes.Role)
               ?? principal.FindFirstValue("role")
               ?? principal.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value
               ?? "Viewer";

        var municipalityIdStr = principal.FindFirstValue("municipalityId");
        MunicipalityId = string.IsNullOrEmpty(municipalityIdStr)
            ? null
            : Guid.Parse(municipalityIdStr);
    }
}