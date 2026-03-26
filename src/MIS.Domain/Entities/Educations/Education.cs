using MIS.Domain.Entities.HouseHold;

namespace MIS.Domain.Entities.Educations;

public class Education
{
  public Guid Id { get; set; }
  public Guid PersonId { get; set; }
  public string Program { get; set; } = null!;
  public DateTime StartYear { get; set; }
  public DateTime EndYear { get; set; }
  public int GradeOrGPA { get; set; }
  public string BoardOrUniversity { get; set; } = null!;

  //Navigation property 
  public Person Person { get; set; } = null!;

}