using FluentAssertions;
using MIS.Application.Features.Geography.Districts;

namespace MIS.Application.Tests.Features.Geography.Districts;

public class CreateDistrictDTOValidatorTests
{
  private readonly CreateDistrictDTOValidator _validator = new();

  [Fact]
  public async Task ValidateAsync_WhenProvinceIdIsEmpty_ShouldReturnValidationError()
  {
    var dto = new CreateDistrictDTO
    {
      ProvinceId = Guid.Empty,
      Code = "D1",
      NameEn = "Kathmandu",
      NameNe = "काठमाण्डौ"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("Province is required");
  }

  [Fact]
  public async Task ValidateAsync_WhenCodeTooLong_ShouldReturnValidationError()
  {
    var dto = new CreateDistrictDTO
    {
      ProvinceId = Guid.NewGuid(),
      Code = new string('D', 21),
      NameEn = "Kathmandu",
      NameNe = "काठमाण्डौ"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("District code must be at most 20 characters");
  }

  [Fact]
  public async Task ValidateAsync_WhenPayloadIsValid_ShouldReturnSuccess()
  {
    var dto = new CreateDistrictDTO
    {
      ProvinceId = Guid.NewGuid(),
      Code = "D1",
      NameEn = "Kathmandu",
      NameNe = "काठमाण्डौ"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeTrue();
    result.Errors.Should().BeEmpty();
  }
}
