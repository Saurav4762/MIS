using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Institute
{
  [Key]
  public Guid Id { get; set; }
  public Guid HouseId { get; set; }
  public string Name { get; set; } = null!;

  // Navigation properties
  public House House { get; set; } = null!;
}