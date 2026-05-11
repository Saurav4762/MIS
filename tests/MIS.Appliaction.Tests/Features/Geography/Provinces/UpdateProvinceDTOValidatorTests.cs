using FluentAssertions;
using MIS.Application.Features.Geography.Provinces;

namespace MIS.Application.Tests.Features.Geography.Provinces;

public class UpdateProvinceDTOValidatorTests
{
  private readonly UpdateProvinceDTOValidator _validator = new();

  [Fact]
  public async Task ValidateAsync_WhenCodeTooLong_ShouldReturnValidationError()
  {
    var dto = new UpdateProvinceDTO
    {
      Code = new string('A', 21)
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("Province code must be at most 20 characters");
  }

  [Fact]
  public async Task ValidateAsync_WhenNameEnTooLong_ShouldReturnValidationError()
  {
    var dto = new UpdateProvinceDTO
    {
      NameEn = new string('B', 201)
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage).Should().Contain("Province English name must be at most 200 characters");
  }

  [Fact]
  public async Task ValidateAsync_WhenPayloadIsValid_ShouldReturnSuccess()
  {
    var dto = new UpdateProvinceDTO
    {
      Code = "P1",
      NameEn = "Koshi",
      NameNe = "कोशी"
    };

    var result = await _validator.ValidateAsync(dto);

    result.IsValid.Should().BeTrue();
    result.Errors.Should().BeEmpty();
  }
}
