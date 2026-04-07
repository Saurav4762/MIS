using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using OfficeOpenXml;


namespace MIS.Infrastructure.ExcelParser;

public class MunicipalityExcelParser : IMunicipalityExcelParser
{
  public List<ExcelMunicipalityDTO> Parse(Stream fileStream)
  {
    var results = new List<ExcelMunicipalityDTO>();

    using var package = new ExcelPackage(fileStream);


    var sheet = package.Workbook.Worksheets[0];

    var Code = sheet.Cells[1, 1].Text;
    var NameEn = sheet.Cells[1, 2].Text;
    var NameNe = sheet.Cells[1, 3].Text;

    if (
      Code != nameof(ExcelMunicipalityDTO.Code)
      || NameEn != nameof(ExcelMunicipalityDTO.NameEn)
      || NameNe != nameof(ExcelMunicipalityDTO.NameNe)
      )
    {
      throw new DataValidationException("File", "File format not matched.");
    }



    int rowCount = sheet.Dimension.Rows;
    for (int row = 2; row <= rowCount; row++)
    {
      results.Add(new ExcelMunicipalityDTO
      {
        RowNumber = row,
        Code = sheet.Cells[row, 1].Text,
        NameEn = sheet.Cells[row, 2].Text,
        NameNe = sheet.Cells[row, 3].Text
      });
    }
    return results;
  }
}
