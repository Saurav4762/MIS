using FluentAssertions;
using MIS.Application.Features.Authentication;

namespace MIS.Application.Tests.Features.Authentication;

public class LoginUserDTOValidatorTests
{
  private readonly LoginUserDTOValidator _validator = new();

  [Fact]
  public async Task ValidateAsync_WhenEmailIsEmpty_ShouldReturnValidationError()
  {
    // Arrange
    var dto = new LoginUserDTO
    {
      Email = string.Empty,
      Password = "valid-password"
    };

    // Act
    var result = await _validator.ValidateAsync(dto);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage)
      .Should().Contain("Email is required");
  }

  [Fact]
  public async Task ValidateAsync_WhenEmailIsInvalid_ShouldReturnValidationError()
  {
    // Arrange
    var dto = new LoginUserDTO
    {
      Email = "invalid-email",
      Password = "valid-password"
    };

    // Act
    var result = await _validator.ValidateAsync(dto);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage)
      .Should().Contain("Invalid email format");
  }

  [Fact]
  public async Task ValidateAsync_WhenPasswordIsEmpty_ShouldReturnValidationError()
  {
    // Arrange
    var dto = new LoginUserDTO
    {
      Email = "user@test.com",
      Password = string.Empty
    };

    // Act
    var result = await _validator.ValidateAsync(dto);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Select(e => e.ErrorMessage)
      .Should().Contain("Password is required");
  }

  [Fact]
  public async Task ValidateAsync_WhenRequestIsValid_ShouldReturnSuccess()
  {
    // Arrange
    var dto = new LoginUserDTO
    {
      Email = "user@test.com",
      Password = "valid-password"
    };

    // Act
    var result = await _validator.ValidateAsync(dto);

    // Assert
    result.IsValid.Should().BeTrue();
    result.Errors.Should().BeEmpty();
  }
}
