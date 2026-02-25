using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Religion
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    // Navigation properties
}