using FluentAssertions;
using MIS.Application.Features.Geography.Provinces;

namespace MIS.Application.Tests.Features.Geography.Provinces;

public class CreateProvinceDTOValidatorTests
{
  private readonly CreateProvinceDTOValidator _validator = new();

  [Fact]
  public async Task ValidateAsync_WhenCodeIsEmpty_ShouldReturnValidationError()
  {
    var dto = new CreateProvinceDTO
    {
      Code = string.Empty,
      NameEn = "Bagmati",
      NameNe = "बागमती"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("Province code is required");
  }

  [Fact]
  public async Task ValidateAsync_WhenCodeIsTooLong_ShouldReturnValidationError()
  {
    var dto = new CreateProvinceDTO
    {
      Code = new string('A', 21),
      NameEn = "Bagmati",
      NameNe = "बागमती"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("Province code must be at most 20 characters");
  }

  [Fact]
  public async Task ValidateAsync_WhenRequestIsValid_ShouldReturnSuccess()
  {
    var dto = new CreateProvinceDTO
    {
      Code = "P3",
      NameEn = "Bagmati",
      NameNe = "बागमती"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeTrue();
    result.Errors.Should().BeEmpty();
  }
}
