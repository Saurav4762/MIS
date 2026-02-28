using Mis.API.Models;

namespace MIS.API.Dtos;


public class ReligionRequestDto
{
    public string NameEn { get; set; }
    
    public string NameNe { get; set; }
    
}

public class ReligionResponseDto
{
    public Guid Id { get; set; }
    
    public string NameEn { get; set; }
    
    public string NameNe { get; set; }
    
}