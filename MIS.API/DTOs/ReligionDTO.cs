using Mis.API.Models;

namespace MIS.API.Dtos;


public class ReligionRequest
{
    public string NameEn { get; set; }
    
    public string NameNe { get; set; }
    
}

public class ReligionResponse
{
    public Guid Id { get; set; }
    
    public string NameEn { get; set; }
    
    public string NameNe { get; set; }
    
}