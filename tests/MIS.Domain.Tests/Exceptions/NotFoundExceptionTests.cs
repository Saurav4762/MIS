using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class NotFoundExceptionTests
{
  [Fact]
  public void Constructor_ShouldSetMessageErrorCodeAndDetails()
  {
    // Act
    var exception = new NotFoundException("Municipality", "Id", 42);

    // Assert
    exception.Message.Should().Be("Municipality not found");
    exception.ErrorCode.Should().Be("NOT_FOUND");
    exception.Details.Should().NotBeNull();
    exception.Details!["Municipality"].Should().ContainSingle()
      .Which.Should().Be("Id with '$42' was not found.");
  }
}
