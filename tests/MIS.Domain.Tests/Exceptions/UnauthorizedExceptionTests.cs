using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class UnauthorizedExceptionTests
{
  [Fact]
  public void Constructor_WithoutMessage_ShouldUseDefaultMessage()
  {
    // Act
    var exception = new UnauthorizedException();

    // Assert
    exception.Message.Should().Be("You are not authenticated. Please log in");
    exception.ErrorCode.Should().Be("UNAUTHORIZED");
    exception.Details.Should().BeNull();
  }

  [Fact]
  public void Constructor_WithCustomMessage_ShouldSetMessage()
  {
    // Act
    var exception = new UnauthorizedException("Token expired");

    // Assert
    exception.Message.Should().Be("Token expired");
    exception.ErrorCode.Should().Be("UNAUTHORIZED");
    exception.Details.Should().BeNull();
  }
}
