using System.ComponentModel;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MIS.Infrastructure.Excel;

/// <summary>
/// Attribute to customize column header names in generated Excel files.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnAttribute(string header, int order = int.MaxValue) : Attribute
{
    public string Header { get; } = header;
    public int Order { get; } = order;
}

/// <summary>
/// Attribute to exclude a property from the Excel output.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelIgnoreAttribute : Attribute;

/// <summary>
/// Options for Excel generation.
/// </summary>
public class ExcelGeneratorOptions
{
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm";
    public bool AutoFitColumns { get; set; } = true;
    public bool FreezeHeaderRow { get; set; } = true;
    public bool AddAutoFilter { get; set; } = true;

    public Color HeaderBackgroundColor { get; set; } = Color.FromArgb(31, 78, 121);   // Dark blue
    public Color HeaderFontColor { get; set; } = Color.White;
    public Color AltRowColor { get; set; } = Color.FromArgb(235, 241, 247);           // Light blue-grey
}

/// <summary>
/// Generates Excel workbooks from strongly-typed collections using reflection.
/// Supports [ExcelColumn], [ExcelIgnore], and [Description] attributes.
/// </summary>
public class ExcelGenerator
{
    private readonly ExcelGeneratorOptions _options;

    public ExcelGenerator(ExcelGeneratorOptions? options = null)
    {
        _options = options ?? new ExcelGeneratorOptions();
    }

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    /// <summary>
    /// Generates a workbook with a single sheet from <paramref name="data"/>.
    /// </summary>
    public byte[] Generate<T>(IEnumerable<T> data, string sheetName = "Sheet1")
    {
        using var package = new ExcelPackage();
        AddSheet(package, data, sheetName);
        return package.GetAsByteArray();
    }

    /// <summary>
    /// Generates a workbook with multiple sheets from a dictionary of sheet name → data.
    /// </summary>
    public byte[] GenerateMultiSheet(IReadOnlyDictionary<string, object> sheets)
    {
        using var package = new ExcelPackage();

        foreach (var (sheetName, data) in sheets)
        {
            var itemType = data.GetType()
                              .GetInterfaces()
                              .FirstOrDefault(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                              ?.GetGenericArguments()[0];

            if (itemType is null) continue;

            var method = typeof(ExcelGenerator)
                .GetMethod(nameof(AddSheet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(itemType);

            method.Invoke(this, [package, data, sheetName]);
        }

        return package.GetAsByteArray();
    }

    // -------------------------------------------------------------------------
    // Core sheet builder
    // -------------------------------------------------------------------------

    private void AddSheet<T>(ExcelPackage package, IEnumerable<T> data, string sheetName)
    {
        var sheet = package.Workbook.Worksheets.Add(sheetName);
        var columns = GetColumns(typeof(T));
        var rows = data.ToList();

        WriteHeaders(sheet, columns);
        WriteData(sheet, columns, rows);
        ApplyPostFormatting(sheet, columns.Count, rows.Count);
    }

    // -------------------------------------------------------------------------
    // Column metadata
    // -------------------------------------------------------------------------

    private record ColumnMeta(string Header, PropertyInfo Property, int Order);

    private static List<ColumnMeta> GetColumns(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<ExcelIgnoreAttribute>() is null)
            .Select(p =>
            {
                var attr = p.GetCustomAttribute<ExcelColumnAttribute>();
                var desc = p.GetCustomAttribute<DescriptionAttribute>();
                var header = attr?.Header ?? desc?.Description ?? ToTitleCase(p.Name);
                var order = attr?.Order ?? int.MaxValue;
                return new ColumnMeta(header, p, order);
            })
            .OrderBy(c => c.Order)
            .ToList();
    }

    // -------------------------------------------------------------------------
    // Header row
    // -------------------------------------------------------------------------

    private void WriteHeaders(ExcelWorksheet sheet, List<ColumnMeta> columns)
    {
        for (int col = 1; col <= columns.Count; col++)
        {
            var cell = sheet.Cells[1, col];
            cell.Value = columns[col - 1].Header;

            cell.Style.Font.Bold = true;
            cell.Style.Font.Color.SetColor(_options.HeaderFontColor);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(_options.HeaderBackgroundColor);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            cell.Style.Border.Bottom.Color.SetColor(Color.White);
        }

        sheet.Row(1).Height = 22;
    }

    // -------------------------------------------------------------------------
    // Data rows
    // -------------------------------------------------------------------------

    private void WriteData<T>(ExcelWorksheet sheet, List<ColumnMeta> columns, List<T> rows)
    {
        for (int rowIdx = 0; rowIdx < rows.Count; rowIdx++)
        {
            int excelRow = rowIdx + 2; // 1-indexed + header offset
            bool isAltRow = rowIdx % 2 == 1;

            for (int col = 1; col <= columns.Count; col++)
            {
                var meta = columns[col - 1];
                var cell = sheet.Cells[excelRow, col];
                var rawValue = meta.Property.GetValue(rows[rowIdx]);

                SetCellValue(cell, rawValue, meta.Property.PropertyType);

                if (isAltRow)
                {
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(_options.AltRowColor);
                }

                // Subtle bottom border
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                cell.Style.Border.Bottom.Color.SetColor(Color.LightGray);
            }
        }
    }

    private void SetCellValue(ExcelRange cell, object? value, Type propertyType)
    {
        if (value is null)
        {
            cell.Value = string.Empty;
            return;
        }

        // Unwrap nullable
        var underlying = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        if (underlying == typeof(DateTime) || underlying == typeof(DateOnly))
        {
            cell.Value = value is DateOnly d ? d.ToDateTime(TimeOnly.MinValue) : (DateTime)value;
            cell.Style.Numberformat.Format = _options.DateFormat;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        else if (underlying == typeof(DateTimeOffset))
        {
            cell.Value = ((DateTimeOffset)value).DateTime;
            cell.Style.Numberformat.Format = _options.DateTimeFormat;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        else if (underlying == typeof(bool))
        {
            cell.Value = (bool)value ? "Yes" : "No";
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        else if (underlying == typeof(decimal) || underlying == typeof(double) || underlying == typeof(float))
        {
            cell.Value = Convert.ToDouble(value);
            cell.Style.Numberformat.Format = "#,##0.00";
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        }
        else if (underlying == typeof(int) || underlying == typeof(long) || underlying == typeof(short))
        {
            cell.Value = Convert.ToInt64(value);
            cell.Style.Numberformat.Format = "#,##0";
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        }
        else if (underlying.IsEnum)
        {
            // Try [Description] attribute on enum member, fallback to name
            var member = underlying.GetMember(value.ToString()!).FirstOrDefault();
            var desc = member?.GetCustomAttribute<DescriptionAttribute>()?.Description;
            cell.Value = desc ?? value.ToString();
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        else
        {
            cell.Value = value.ToString();
        }
    }

    // -------------------------------------------------------------------------
    // Post-formatting
    // -------------------------------------------------------------------------

    private void ApplyPostFormatting(ExcelWorksheet sheet, int colCount, int rowCount)
    {
        if (rowCount == 0) return;

        var dataRange = sheet.Cells[1, 1, rowCount + 1, colCount];

        if (_options.AddAutoFilter)
            dataRange.AutoFilter = true;

        if (_options.FreezeHeaderRow)
            sheet.View.FreezePanes(2, 1);

        if (_options.AutoFitColumns)
        {
            for (int col = 1; col <= colCount; col++)
            {
                sheet.Column(col).AutoFit(10, 60); // min 10, max 60 chars wide
            }
        }
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private static string ToTitleCase(string camelCase)
    {
        if (string.IsNullOrEmpty(camelCase)) return camelCase;
        return string.Concat(camelCase.Select((c, i) =>
            i > 0 && char.IsUpper(c) ? " " + c : c.ToString()));
    }
}