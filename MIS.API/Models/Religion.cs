using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Religion
{
    [Key]
    public Guid Id { get; set; }
    public string NameNe { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    
    
    
    // Navigation properties
    
    public IEnumerable<Family> Families { get; set; }
}