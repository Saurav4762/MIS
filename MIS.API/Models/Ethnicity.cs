using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Ethnicity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    // Navigation properties
   }