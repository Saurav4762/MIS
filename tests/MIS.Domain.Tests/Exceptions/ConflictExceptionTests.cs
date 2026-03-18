using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class ConflictExceptionTests
{
  [Fact]
  public void Constructor_WithEntityAndReason_ShouldSetDefaultConflictMessageAndDetails()
  {
    // Act
    var exception = new ConflictException("User", "Email already exists");

    // Assert
    exception.Message.Should().Be("The request could not be completed because it conflicts with the current state of the resource.");
    exception.ErrorCode.Should().Be("CONFLICT");
    exception.Details.Should().NotBeNull();
    exception.Details!["User"].Should().ContainSingle().Which.Should().Be("Email already exists");
  }

  [Fact]
  public void Constructor_WithoutMessage_ShouldUseDefaultMessage()
  {
    // Act
    var exception = new ConflictException();

    // Assert
    exception.Message.Should().Be("The request could not be completed because it conflicts with the current state of the resource.");
    exception.ErrorCode.Should().Be("CONFLICT");
    exception.Details.Should().BeNull();
  }

  [Fact]
  public void Constructor_WithCustomMessage_ShouldSetCustomMessage()
  {
    // Act
    var exception = new ConflictException("Conflict happened");

    // Assert
    exception.Message.Should().Be("Conflict happened");
    exception.ErrorCode.Should().Be("CONFLICT");
    exception.Details.Should().BeNull();
  }
}
