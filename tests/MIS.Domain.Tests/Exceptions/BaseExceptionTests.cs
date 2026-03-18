using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class BaseExceptionTests
{
  private sealed class TestBaseException : BaseException
  {
    public TestBaseException(string message, string errorCode, Dictionary<string, string[]>? details = null)
      : base(message, errorCode, details)
    {
    }
  }

  [Fact]
  public void Constructor_WithMessageAndErrorCode_ShouldSetProperties()
  {
    // Act
    var exception = new TestBaseException("test-message", "TEST_CODE");

    // Assert
    exception.Message.Should().Be("test-message");
    exception.ErrorCode.Should().Be("TEST_CODE");
    exception.Details.Should().BeNull();
  }

  [Fact]
  public void Constructor_WithDetails_ShouldSetDetailsReference()
  {
    // Arrange
    var details = new Dictionary<string, string[]>
    {
      ["field"] = ["error"]
    };

    // Act
    var exception = new TestBaseException("test-message", "TEST_CODE", details);

    // Assert
    exception.Details.Should().BeSameAs(details);
  }
}
