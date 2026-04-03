
namespace MIS.Application.Features.Geography.Municipalities;

public record ExcelMunicipalityDTO : CreateMunicipalityDTO
{
  public int RowNumber { get; set; }
}