namespace MIS.Application.Features.Reports;

/// <summary>
/// Interface for report generation service
/// All methods return ChartDataResponse for frontend chart integration
/// </summary>
public interface IReportService
{
    // ============ POPULATION REPORTS ============
    
    /// <summary>
    /// Get population distribution by gender for a municipality
    /// </summary>
    Task<ChartDataResponse> GetPopulationByGenderAsync(Guid municipalityId);

    /// <summary>
    /// Get population distribution by age groups
    /// </summary>
    Task<ChartDataResponse> GetPopulationByAgeGroupAsync(Guid municipalityId);

    /// <summary>
    /// Get population distribution by education level
    /// </summary>
    Task<ChartDataResponse> GetPopulationByEducationAsync(Guid municipalityId);

    /// <summary>
    /// Get literacy rate for a municipality
    /// </summary>
    Task<ChartDataResponse> GetLiteracyRateAsync(Guid municipalityId);

    /// <summary>
    /// Get population by gender and ward
    /// </summary>
    Task<ChartDataResponse> GetPopulationByGenderAndWardAsync(Guid municipalityId);

    // ============ HOUSEHOLD REPORTS ============

    /// <summary>
    /// Get household count by ward
    /// </summary>
    Task<ChartDataResponse> GetHouseholdsByWardAsync(Guid municipalityId);

    /// <summary>
    /// Get households by ownership type
    /// </summary>
    Task<ChartDataResponse> GetHouseholdsByOwnershipAsync(Guid wardId);

    /// <summary>
    /// Get average family size
    /// </summary>
    Task<ChartDataResponse> GetAverageFamilySizeAsync(Guid municipalityId);

    /// <summary>
    /// Get household trend over time
    /// </summary>
    Task<ChartDataResponse> GetHouseholdTrendAsync(Guid municipalityId);

    // ============ HOUSE REPORTS ============

    /// <summary>
    /// Get distribution of house types
    /// </summary>
    Task<ChartDataResponse> GetHouseTypeDistributionAsync(Guid wardId);

    /// <summary>
    /// Get distribution of roof types
    /// </summary>
    Task<ChartDataResponse> GetRoofTypeDistributionAsync(Guid wardId);

    /// <summary>
    /// Get distribution of wall materials
    /// </summary>
    Task<ChartDataResponse> GetWallMaterialDistributionAsync(Guid wardId);

    /// <summary>
    /// Get distribution of land types
    /// </summary>
    Task<ChartDataResponse> GetLandTypeDistributionAsync(Guid wardId);

    /// <summary>
    /// Get house quality indicators
    /// </summary>
    Task<ChartDataResponse> GetHouseQualityAsync(Guid wardId);

    // ============ FAMILY REPORTS ============

    /// <summary>
    /// Get families by ethnicity
    /// </summary>
    Task<ChartDataResponse> GetFamiliesByEthnicityAsync(Guid municipalityId);

    /// <summary>
    /// Get families by religion
    /// </summary>
    Task<ChartDataResponse> GetFamiliesByReligionAsync(Guid municipalityId);

    // ============ GEOGRAPHIC REPORTS ============

    /// <summary>
    /// Get geographic coverage by ward
    /// </summary>
    Task<ChartDataResponse> GetGeographicCoverageAsync(Guid municipalityId);

    /// <summary>
    /// Get population by ward
    /// </summary>
    Task<ChartDataResponse> GetPopulationByWardAsync(Guid municipalityId);

    /// <summary>
    /// Get ward hierarchy
    /// </summary>
    Task<ChartDataResponse> GetWardHierarchyAsync(Guid municipalityId);

    // ============ DATA QUALITY REPORTS ============

    /// <summary>
    /// Get data quality metrics
    /// </summary>
    Task<ChartDataResponse> GetDataQualityMetricsAsync(Guid municipalityId);

    /// <summary>
    /// Get submission status
    /// </summary>
    Task<ChartDataResponse> GetSubmissionStatusAsync(Guid municipalityId);

    /// <summary>
    /// Get data completeness by section
    /// </summary>
    Task<ChartDataResponse> GetDataCompletenessAsync(Guid municipalityId);
    
    //======================DASHBOARD REPORTS======================
    
    Task<DashboardStatsResponse> GetDashboardStatsAsync(Guid municipalityId);
}


