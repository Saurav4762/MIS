namespace MIS.API.DTOs;

public class WardDTO
{
   public class WardRequest
   {
      public Guid MunicipalityId { get; set; }
      public string Name { get; set; } = null!;

      public string Code { get; set; } = null!;
   }

   public class WardResponse
   {
      public Guid Id { get; set; } 
      
      public Guid MunicipalityId {get; set;}
      
      public string MunicipalityCode {get; set;} = null!;
      
      public string MunicipalityName {get; set;} = null!;
      
      public string WardName { get; set; } = null!;
      
      public string WardCode { get; set; } = null!;
   }
}