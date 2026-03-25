using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.Geography.Municipalities;
public interface IMunicipalityExcelParser
{
  List<CreateMunicipalityDTO> Parse(Stream fileStream);
}