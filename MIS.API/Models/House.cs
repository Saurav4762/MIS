using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace MIS.API.Models;

public class House
{
    [Key]
    public Guid Id { get; set; }
}