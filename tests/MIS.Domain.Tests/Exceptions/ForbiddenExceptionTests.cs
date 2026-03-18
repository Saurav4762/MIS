using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class ForbiddenExceptionTests
{
  [Fact]
  public void Constructor_WithoutMessage_ShouldUseDefaultMessage()
  {
    // Act
    var exception = new ForbiddenException();

    // Assert
    exception.Message.Should().Be("You do not have permission to access this resource.");
    exception.ErrorCode.Should().Be("FORBIDDEN");
    exception.Details.Should().BeNull();
  }

  [Fact]
  public void Constructor_WithCustomMessage_ShouldSetMessage()
  {
    // Act
    var exception = new ForbiddenException("Access denied for this operation.");

    // Assert
    exception.Message.Should().Be("Access denied for this operation.");
    exception.ErrorCode.Should().Be("FORBIDDEN");
    exception.Details.Should().BeNull();
  }
}
