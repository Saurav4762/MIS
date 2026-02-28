namespace MIS.API.DTOs;

public class EthnicityRequest
{
    public string NameEn { get; set; } = null!;
    public string NameNe { get; set; } = null!;
}

public class EthnicityResponse
{
    public string NameEn { get; set; }
    public string NameNe { get; set; }
}