using Mis.API.Models;

namespace MIS.API.DTOs;


public class ReligionRequest
{
    public string NameEn { get; set; }= null!;
    
    public string NameNe { get; set; }= null!;
    
}

public class ReligionResponse
{
    public Guid Id { get; set; }
    
    public string NameEn { get; set; } = null!;
    
    public string NameNe { get; set; }= null !;
    
}