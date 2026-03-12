namespace MIS.API.DTOs;

public class WardDTO
{
   public class WardRequest
   {
      public Guid MunicipalityId { get; set; }
      public string Name { get; set; } = string.Empty;

      public string Code { get; set; } = string.Empty;
   }

   public class WardResponse
   {
      public Guid Id { get; set; } 
      
      public Guid MunicipalityId {get; set;}
      
      public string MunicipalityCode {get; set;} = string.Empty;
      
      public string MunicipalityName {get; set;} = string.Empty;
      
      public string WardName { get; set; } = string.Empty;
      
      public string WardCode { get; set; } = string.Empty;
   }
}