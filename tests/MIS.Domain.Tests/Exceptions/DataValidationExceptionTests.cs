using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class DataValidationExceptionTests
{
  [Fact]
  public void Constructor_WithDictionary_ShouldSetValidationMessageErrorCodeAndDetails()
  {
    // Arrange
    var errors = new Dictionary<string, string[]>
    {
      ["Email"] = ["Email is required."]
    };

    // Act
    var exception = new DataValidationException(errors);

    // Assert
    exception.Message.Should().Be("One or more validation error occured.");
    exception.ErrorCode.Should().Be("VALIDATION_ERROR");
    exception.Details.Should().BeSameAs(errors);
  }

  [Fact]
  public void Constructor_WithFieldAndError_ShouldCreateSingleDetailsEntry()
  {
    // Act
    var exception = new DataValidationException("Password", "Password is too weak.");

    // Assert
    exception.Message.Should().Be("A validation error occured.");
    exception.ErrorCode.Should().Be("VALIDATION_ERROR");
    exception.Details.Should().NotBeNull();
    exception.Details!.Should().ContainKey("Password");
    exception.Details["Password"].Should().ContainSingle()
      .Which.Should().Be("Password is too weak.");
  }
}
