using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.API.Common;
using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.API.Features.Reports;

/// <summary>
/// Reports API Controller
/// Provides endpoints for generating dynamic charts and reports
/// All data is pulled from database in real-time
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly ApplicationDbContext _context;

    public ReportsController(IReportService reportService, ApplicationDbContext context)
    {
        _reportService = reportService;
        _context = context;
    }

    // ============ POPULATION ENDPOINTS ============

    /// <summary>
    /// Get population distribution by gender for a municipality
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Gender distribution data</returns>
    [HttpGet("population/by-gender/{municipalityId}")]
    public async Task<IActionResult> GetPopulationByGender(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetPopulationByGenderAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get population distribution by age groups for a municipality
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Age group distribution data</returns>
    [HttpGet("population/by-age-group/{municipalityId}")]
    public async Task<IActionResult> GetPopulationByAgeGroup(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetPopulationByAgeGroupAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get population distribution by education level
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Education level distribution</returns>
    [HttpGet("population/by-education/{municipalityId}")]
    public async Task<IActionResult> GetPopulationByEducation(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetPopulationByEducationAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get literacy rate for a municipality
    /// Chart Type: Doughnut Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Literacy rate data</returns>
    [HttpGet("population/literacy-rate/{municipalityId}")]
    public async Task<IActionResult> GetLiteracyRate(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetLiteracyRateAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get population by gender and ward
    /// Chart Type: Bar Chart (Grouped)
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Population by gender per ward</returns>
    [HttpGet("population/by-gender-and-ward/{municipalityId}")]
    public async Task<IActionResult> GetPopulationByGenderAndWard(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetPopulationByGenderAndWardAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ============ HOUSEHOLD ENDPOINTS ============

    /// <summary>
    /// Get household count by ward
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Households by ward</returns>
    [HttpGet("households/by-ward/{municipalityId}")]
    public async Task<IActionResult> GetHouseholdsByWard(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetHouseholdsByWardAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get households by ownership type
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>Households by ownership</returns>
    [HttpGet("households/by-ownership/{wardId}")]
    public async Task<IActionResult> GetHouseholdsByOwnership(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetHouseholdsByOwnershipAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get average family size
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Family size data</returns>
    [HttpGet("households/average-family-size/{municipalityId}")]
    public async Task<IActionResult> GetAverageFamilySize(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetAverageFamilySizeAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get household creation trend
    /// Chart Type: Line Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Household trend data</returns>
    [HttpGet("households/trend/{municipalityId}")]
    public async Task<IActionResult> GetHouseholdTrend(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetHouseholdTrendAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ============ HOUSE ENDPOINTS ============

    /// <summary>
    /// Get distribution of house types
    /// Chart Type: Doughnut Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>House types distribution</returns>
    [HttpGet("houses/by-type/{wardId}")]
    public async Task<IActionResult> GetHouseTypeDistribution(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetHouseTypeDistributionAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get distribution of roof types
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>Roof types distribution</returns>
    [HttpGet("houses/by-roof-type/{wardId}")]
    public async Task<IActionResult> GetRoofTypeDistribution(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetRoofTypeDistributionAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get distribution of wall materials
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>Wall materials distribution</returns>
    [HttpGet("houses/by-wall-material/{wardId}")]
    public async Task<IActionResult> GetWallMaterialDistribution(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetWallMaterialDistributionAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get distribution of land types
    /// Chart Type: Doughnut Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>Land types distribution</returns>
    [HttpGet("houses/by-land-type/{wardId}")]
    public async Task<IActionResult> GetLandTypeDistribution(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetLandTypeDistributionAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get house quality indicators
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="wardId">Ward ID</param>
    /// <returns>House quality data</returns>
    [HttpGet("houses/quality/{wardId}")]
    public async Task<IActionResult> GetHouseQuality(Guid wardId)
    {
        try
        {
            var report = await _reportService.GetHouseQualityAsync(wardId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ============ FAMILY ENDPOINTS ============

    /// <summary>
    /// Get families by ethnicity
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Families by ethnicity</returns>
    [HttpGet("families/by-ethnicity/{municipalityId}")]
    public async Task<IActionResult> GetFamiliesByEthnicity(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetFamiliesByEthnicityAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get families by religion
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Families by religion</returns>
    [HttpGet("families/by-religion/{municipalityId}")]
    public async Task<IActionResult> GetFamiliesByReligion(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetFamiliesByReligionAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ============ GEOGRAPHIC ENDPOINTS ============

    /// <summary>
    /// Get geographic coverage by ward
    /// Chart Type: Doughnut Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Geographic coverage data</returns>
    [HttpGet("geographic/coverage/{municipalityId}")]
    public async Task<IActionResult> GetGeographicCoverage(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetGeographicCoverageAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get population by ward
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Population per ward</returns>
    [HttpGet("geographic/population-by-ward/{municipalityId}")]
    public async Task<IActionResult> GetPopulationByWard(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetPopulationByWardAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get ward hierarchy (Areas and Toles)
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Ward hierarchy data</returns>
    [HttpGet("geographic/ward-hierarchy/{municipalityId}")]
    public async Task<IActionResult> GetWardHierarchy(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetWardHierarchyAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ============ DATA QUALITY ENDPOINTS ============

    /// <summary>
    /// Get data quality metrics
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Data quality metrics</returns>
    [HttpGet("quality/metrics/{municipalityId}")]
    public async Task<IActionResult> GetDataQualityMetrics(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetDataQualityMetricsAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get submission status
    /// Chart Type: Pie Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Submission status data</returns>
    [HttpGet("submissions/status/{municipalityId}")]
    public async Task<IActionResult> GetSubmissionStatus(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetSubmissionStatusAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get data completeness by section
    /// Chart Type: Bar Chart
    /// </summary>
    /// <param name="municipalityId">Municipality ID</param>
    /// <returns>Data completeness by section</returns>
    [HttpGet("quality/completeness/{municipalityId}")]
    public async Task<IActionResult> GetDataCompleteness(Guid municipalityId)
    {
        try
        {
            var report = await _reportService.GetDataCompletenessAsync(municipalityId);
            return Ok(new { success = true, data = report });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Get dashboard stats from a municipality
    /// </summary>

    [AllowAnonymous]
[HttpGet("dashboard/stats")]
public async Task<IActionResult> GetDashboardStats([FromQuery] Guid? municipalityId)
{
    try
    {
        Guid id;

        // If municipalityId is passed as query param (public access)
        if (municipalityId.HasValue)
        {
            id = municipalityId.Value;
        }
        else if (User.Identity?.IsAuthenticated == true)
        {
            var currentUser = new CurrentUser(User);
            if (currentUser.IsSuperAdmin)
            {
                var firstMunicipality = await _context.Set<Municipality>()
                    .FirstOrDefaultAsync();
                if (firstMunicipality == null)
                    return BadRequest(new { success = false, message = "No municipality found" });
                id = firstMunicipality.Id;
            }
            else
            {
                if (!currentUser.MunicipalityId.HasValue)
                    return BadRequest(new { success = false, message = "User has no municipality assigned" });
                id = currentUser.MunicipalityId.Value;
            }
        }
        else
        {
            return BadRequest(new { success = false, message = "Municipality ID required" });
        }

        var stats = await _reportService.GetDashboardStatsAsync(id);
        return Ok(new
        {
            data = new
            {
                populationByGender = new
                {
                    male = stats.PopulationByGender.Male,
                    female = stats.PopulationByGender.Female,
                    others = stats.PopulationByGender.Others
                },
                totalHouseholds = stats.TotalHouseholds,
                totalPopulation = stats.TotalPopulation,
                ageGroup16Plus = stats.AgeGroup16Plus,
                literate = stats.Literate,
                jobless = stats.Jobless
            },
            timestamp = DateTime.UtcNow.ToString("o")
        });
    }
    catch (Exception ex)
    {
        return BadRequest(new { success = false, message = ex.Message });
    }
}
    
}
