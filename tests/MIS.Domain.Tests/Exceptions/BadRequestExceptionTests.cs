using FluentAssertions;
using MIS.Domain.Exceptions;

namespace MIS.Domain.Tests.Exceptions;

public class BadRequestExceptionTests
{
	[Fact]
	public void Constructor_WithoutParameters_ShouldSetDefaultMessageErrorCodeAndNullDetails()
	{
		// Act
		var exception = new BadRequestException();

		// Assert
		exception.Message.Should().Be("something went wrong");
		exception.ErrorCode.Should().Be("BAD_REQUEST");
		exception.Details.Should().BeNull();
	}

	[Fact]
	public void Constructor_WithMessage_ShouldSetMessageAndErrorCodeAndNullDetails()
	{
		// Arrange
		var message = "Invalid request payload.";

		// Act
		var exception = new BadRequestException(message);

		// Assert
		exception.Message.Should().Be(message);
		exception.ErrorCode.Should().Be("BAD_REQUEST");
		exception.Details.Should().BeNull();
	}

	[Fact]
	public void Constructor_WithMessageAndDetails_ShouldSetAllProperties()
	{
		// Arrange
		var message = "Validation failed.";
		var details = new Dictionary<string, string[]>
		{
			["Email"] = ["Email is required."],
			["Password"] = ["Password is required.", "Password must be at least 8 characters."]
		};

		// Act
		var exception = new BadRequestException(message, details);

		// Assert
		exception.Message.Should().Be(message);
		exception.ErrorCode.Should().Be("BAD_REQUEST");
		exception.Details.Should().BeSameAs(details);
	}

	[Fact]
	public void Constructor_WithMessageAndDetails_ShouldAllowEmptyDetails()
	{
		// Arrange
		var message = "No fields provided.";
		var details = new Dictionary<string, string[]>();

		// Act
		var exception = new BadRequestException(message, details);

		// Assert
		exception.Details.Should().NotBeNull();
		exception.Details.Should().BeEmpty();
	}

	[Fact]
	public void Constructor_WithEmptyMessage_ShouldPreserveEmptyMessage()
	{
		// Act
		var exception = new BadRequestException(string.Empty);

		// Assert
		exception.Message.Should().Be(string.Empty);
		exception.ErrorCode.Should().Be("BAD_REQUEST");
		exception.Details.Should().BeNull();
	}

	[Fact]
	public void BadRequestException_ShouldInheritFromBaseExceptionAndException()
	{
		// Act
		var exception = new BadRequestException();

		// Assert
		exception.Should().BeAssignableTo<BaseException>();
		exception.Should().BeAssignableTo<Exception>();
	}
}