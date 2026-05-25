namespace MIS.Application.Features.Reports;

/// <summary>
/// Base chart data response for all reports
/// </summary>
public class ChartDataResponse
{
    public string ChartType { get; set; } = "bar"; // bar, pie, line, area, doughnut
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Labels { get; set; } = new();
    public List<Dataset> Datasets { get; set; } = new();
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public long TotalRecords { get; set; }
}

/// <summary>
/// Dataset for chart visualization
/// </summary>
public class Dataset
{
    public string Label { get; set; } = string.Empty;
    public List<int> Data { get; set; } = new();
    public string BackgroundColor { get; set; } = "#3498db";
    public string BorderColor { get; set; } = "#2980b9";
    public int BorderWidth { get; set; } = 1;
}

/// <summary>
/// Population by Gender Report
/// </summary>
public class PopulationByGenderDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public int MaleCount { get; set; }
    public int FemaleCount { get; set; }
    public int OtherCount { get; set; }
    public int TotalPopulation { get; set; }
}

/// <summary>
/// Population by Age Groups Report
/// </summary>
public class PopulationByAgeGroupDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public int Age0To5 { get; set; }
    public int Age6To18 { get; set; }
    public int Age19To60 { get; set; }
    public int Age60Plus { get; set; }
    public int TotalPopulation { get; set; }
}

/// <summary>
/// Population by Education Level Report
/// </summary>
public class PopulationByEducationDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public string EducationLevel { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// Household Statistics Report
/// </summary>
public class HouseholdStatsDTO
{
    public string WardName { get; set; } = string.Empty;
    public int TotalHouseholds { get; set; }
    public int AverageFamilySize { get; set; }
    public Dictionary<string, int> HouseholdsByOwnership { get; set; } = new();
}

/// <summary>
/// House Type Distribution Report
/// </summary>
public class HouseTypeDistributionDTO
{
    public string HouseType { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}

/// <summary>
/// Family by Ethnicity Report
/// </summary>
public class FamilyByEthnicityDTO
{
    public string Ethnicity { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}

/// <summary>
/// Family by Religion Report
/// </summary>
public class FamilyByReligionDTO
{
    public string Religion { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}

/// <summary>
/// Geographic Coverage Report
/// </summary>
public class GeographicCoverageDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public int TotalWards { get; set; }
    public int CoveredWards { get; set; }
    public decimal CoveragePercentage { get; set; }
}

/// <summary>
/// Data Quality Metrics Report
/// </summary>
public class DataQualityMetricsDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public int TotalRecords { get; set; }
    public int CompleteRecords { get; set; }
    public decimal CompletionPercentage { get; set; }
    public int IncompleteRecords { get; set; }
}

/// <summary>
/// Submission Status Report
/// </summary>
public class SubmissionStatusDTO
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// Population by Gender and Ward Report
/// </summary>
public class PopulationByGenderAndWardDTO
{
    public string WardName { get; set; } = string.Empty;
    public int Males { get; set; }
    public int Females { get; set; }
    public int Others { get; set; }
}

/// <summary>
/// Literacy Rate Report
/// </summary>
public class LiteracyRateDTO
{
    public string MunicipalityName { get; set; } = string.Empty;
    public int LiterateCount { get; set; }
    public int IlliterateCount { get; set; }
    public decimal LiteracyRate { get; set; }
}

/// <summary>
/// House Quality Report
/// </summary>
public class HouseQualityDTO
{
    public string Category { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}

