namespace MIS.API.DTOs;

public class WardDTO
{
   public class WardRequest
   {
      public string Name { get; set; } = null!;

      public string Code { get; set; } = null!;
   }

   public class WardResponse
   {
      public Guid Id { get; set; } 
      
      public Guid MunicipalityId {get; set;}
      
      public string Name { get; set; } = null!;
      
      public string Code { get; set; } = null!;
   }
}