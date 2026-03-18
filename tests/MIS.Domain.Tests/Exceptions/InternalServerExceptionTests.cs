using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class InternalServerExceptionTests
{
  [Fact]
  public void Constructor_WithoutMessage_ShouldUseDefaultMessage()
  {
    // Act
    var exception = new InternalServerException();

    // Assert
    exception.Message.Should().Be("An unexpected error occurred. Please try again later.");
    exception.ErrorCode.Should().Be("INTERNAL_ERROR");
    exception.Details.Should().BeNull();
  }

  [Fact]
  public void Constructor_WithCustomMessage_ShouldSetMessage()
  {
    // Act
    var exception = new InternalServerException("Server is down.");

    // Assert
    exception.Message.Should().Be("Server is down.");
    exception.ErrorCode.Should().Be("INTERNAL_ERROR");
    exception.Details.Should().BeNull();
  }
}
