namespace MIS.Application.Features.Reports;

public class DashboardStatsResponse
{
    public PopulationByGender PopulationByGender { get; set; } = new();
    
    public int TotalHouseholds { get; set; }
    public int TotalPopulation { get; set; }
    public int AgeGroup16Plus { get; set; }
    public int Literate { get; set; }
    public int Jobless { get; set; }
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("o");
}

public class PopulationByGender
{
    public int Male { get; set; }
    public int Female { get; set; }
    public int Others { get; set; }
}