using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Ethnicity
{
    [Key]
    public Guid Id { get; set; }
    public string NameEn { get; set; } = null!;
    public string NameNe { get; set; } = null!;

    // Navigation properties
    public IEnumerable<Family> Families { get; set; } = null!;
}