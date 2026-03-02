namespace MIS.API.DTOs;

public class MunicipalityRequest
{
    public string NameEn { get; set; } = null!;
    public string NameNe { get; set; } = null!;
    public string Code { get; set; } = null!;
}

public class MunicipalityResponse
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = null!;
    public string NameNe { get; set; } = null!;
    public string Code { get; set; } = null!;
}