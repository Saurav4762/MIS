using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Exceptions;
using OfficeOpenXml;


namespace MIS.Infrastructure.ExcelParser;

public class MunicipalityExcelParser : IMunicipalityExcelParser
{
  public List<ExcelMunicipalityDTO> Parse(Stream fileStream)
  {
    var results = new List<ExcelMunicipalityDTO>();

    using var package = new ExcelPackage(fileStream);


    var sheet = package.Workbook.Worksheets[0];

    // var Code = sheet.Cells[1, 1].Text;
    // var NameEn = sheet.Cells[1, 2].Text;
    // var NameNe = sheet.Cells[1, 3].Text;
    // var HeadExecutiveNameEn = sheet.Cells[1, 4].Text;
    // var HeadExecutiveNameNe = sheet.Cells[1, 5].Text;
    // var Email = sheet.Cells[1, 6].Text;
    // var PhoneNo = sheet.Cells[1, 7].Text;
    // var Website = sheet.Cells[1, 8].Text;


    var CodeHeader = sheet.Cells[1, 1].Text;
    var NameEnHeader = sheet.Cells[1, 2].Text;
    var NameNeHeader = sheet.Cells[1, 3].Text;
    var HeadExecutiveNameEnHeader = sheet.Cells[1, 4].Text;
    var HeadExecutiveNameNeHeader = sheet.Cells[1, 5].Text;
    var EmailHeader = sheet.Cells[1, 6].Text;
    var PhoneNoHeader = sheet.Cells[1, 7].Text;
    var WebsiteHeader = sheet.Cells[1, 8].Text;



    if (
        CodeHeader != nameof(ExcelMunicipalityDTO.Code) ||
        NameEnHeader != nameof(ExcelMunicipalityDTO.NameEn) ||
        NameNeHeader != nameof(ExcelMunicipalityDTO.NameNe) ||
        HeadExecutiveNameEnHeader != nameof(ExcelMunicipalityDTO.HeadExecutiveNameEn) ||
        HeadExecutiveNameNeHeader != nameof(ExcelMunicipalityDTO.HeadExecutiveNameNe) ||
        EmailHeader != nameof(ExcelMunicipalityDTO.Email) ||
        PhoneNoHeader != nameof(ExcelMunicipalityDTO.PhoneNo) ||
        WebsiteHeader != nameof(ExcelMunicipalityDTO.Website)
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
        NameNe = sheet.Cells[row, 3].Text,
        HeadExecutiveNameEn = sheet.Cells[row, 4].Text,
        HeadExecutiveNameNe = sheet.Cells[row, 5].Text,
        Email = sheet.Cells[row, 6].Text,
        PhoneNo = sheet.Cells[row, 7].Text,
        Website = sheet.Cells[row, 8].Text

      });
    }

    return results;
  }
}
