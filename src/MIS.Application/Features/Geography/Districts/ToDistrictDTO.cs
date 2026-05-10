namespace MIS.Application.Features.Geography.Districts;

public static class ToDTO
{
  public static DistrictDTO ToDistrictDTO(this Domain.Entities.Geography.District district)
  {
    return new DistrictDTO
    {
      Id = district.Id,
      ProvinceId = district.ProvinceId,
      Code = district.Code,
      NameEn = district.NameEn,
      NameNe = district.NameNe
    };
  }

}