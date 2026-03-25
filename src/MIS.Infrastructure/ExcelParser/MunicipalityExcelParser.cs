using MIS.Application.Features.Geography.Municipalities;
using OfficeOpenXml;


namespace MIS.Infrastructure.ExcelParser;

public class MunicipalityExcelParser : IMunicipalityExcelParser
{
  public List<CreateMunicipalityDTO> Parse(Stream fileStream)
  {
    var results = new List<CreateMunicipalityDTO>();

    using var package = new ExcelPackage(fileStream);
    var sheet = package.Workbook.Worksheets[0];
    int rowCount = sheet.Dimension.Rows;
    for (int row = 2; row <= rowCount; row++)
    {
      results.Add(new CreateMunicipalityDTO
      {
        Code = sheet.Cells[row, 1].Text,
        NameEn = sheet.Cells[row, 2].Text,
        NameNe = sheet.Cells[row, 3].Text
      });
    }
    return results;
  }
}
