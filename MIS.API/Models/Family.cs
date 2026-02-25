
namespace MIS.API.Models;
using System.ComponentModel.DataAnnotations;
public class Family
{
    [Key]
    public Guid Id { get; set; }
    public Guid HouseId { get; set; }
    public Guid HeadOfTheFamilyId { get; set; }

    public Guid EthnicityId { get; set; }
    public Guid ReligionId { get; set; }
    
    //Navigation Property
    public Ethnicity Ethnicity { get; set; }
    
    public Religion Religion { get; set; }
    
    public Person HeadOfTheFamily { get; set; }
    
}
