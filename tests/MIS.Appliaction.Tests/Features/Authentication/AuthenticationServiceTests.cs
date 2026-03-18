using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using MIS.Application.Features.Authentication;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Exceptions;

namespace MIS.Application.Tests.Features.Authentication;

public class AuthenticationServiceTests
{
  private readonly Mock<IAuthenticationRepository> _repoMock = new();
  private readonly Mock<IPasswordHashService> _passwordHashServiceMock = new();
  private readonly Mock<IValidator<LoginUserDTO>> _loginValidatorMock = new();
  private readonly Mock<IJwtTokenService> _jwtTokenServiceMock = new();

  private AuthenticationService CreateSut()
  {
    return new AuthenticationService(
      _repoMock.Object,
      _passwordHashServiceMock.Object,
      _loginValidatorMock.Object,
      _jwtTokenServiceMock.Object);
  }

  [Fact]
  public async Task LoginAsync_WhenValidationFails_ShouldThrowDataValidationException()
  {
    // Arrange
    var dto = new LoginUserDTO { Email = "", Password = "" };
    var failures = new List<ValidationFailure>
    {
      new("Email", "Email is required"),
      new("Password", "Password is required")
    };

    _loginValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult(failures));

    var sut = CreateSut();

    // Act
    var act = async () => await sut.LoginAsync(dto);

    // Assert
    await act.Should().ThrowAsync<DataValidationException>();
    _repoMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Never);
  }

  [Fact]
  public async Task LoginAsync_WhenUserNotFound_ShouldThrowUnauthorizedException()
  {
    // Arrange
    var dto = new LoginUserDTO { Email = "user@test.com", Password = "secret" };

    _loginValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock
      .Setup(r => r.GetByEmailAsync(dto.Email))
      .ReturnsAsync((User?)null);

    var sut = CreateSut();

    // Act
    var act = async () => await sut.LoginAsync(dto);

    // Assert
    var exception = await act.Should().ThrowAsync<UnauthorizedException>();
    exception.Which.Message.Should().Be("Invalid email or password");
  }

  [Fact]
  public async Task LoginAsync_WhenPasswordDoesNotMatch_ShouldThrowUnauthorizedException()
  {
    // Arrange
    var dto = new LoginUserDTO { Email = "user@test.com", Password = "wrong-pass" };
    var user = new User { Id = Guid.NewGuid(), Email = dto.Email, PasswordHash = "stored-hash" };

    _loginValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock
      .Setup(r => r.GetByEmailAsync(dto.Email))
      .ReturnsAsync(user);

    _passwordHashServiceMock
      .Setup(p => p.Verify(dto.Password, user.PasswordHash))
      .Returns(false);

    var sut = CreateSut();

    // Act
    var act = async () => await sut.LoginAsync(dto);

    // Assert
    var exception = await act.Should().ThrowAsync<UnauthorizedException>();
    exception.Which.Message.Should().Be("Invalid email or password");
    _jwtTokenServiceMock.Verify(j => j.GenerateToken(It.IsAny<User>()), Times.Never);
  }

  [Fact]
  public async Task LoginAsync_WhenCredentialsAreValid_ShouldReturnAuthResult()
  {
    // Arrange
    var dto = new LoginUserDTO { Email = "user@test.com", Password = "valid-pass" };
    var user = new User
    {
      Id = Guid.NewGuid(),
      Email = dto.Email,
      PasswordHash = "stored-hash",
      FullName = "Test User",
      Username = "testuser",
      Phone = "9800000000"
    };

    var expectedResult = new AuthResultDTO
    {
      AccessToken = "access-token",
      ExpiresAtUtc = DateTime.UtcNow.AddHours(1)
    };

    _loginValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock
      .Setup(r => r.GetByEmailAsync(dto.Email))
      .ReturnsAsync(user);

    _passwordHashServiceMock
      .Setup(p => p.Verify(dto.Password, user.PasswordHash))
      .Returns(true);

    _jwtTokenServiceMock
      .Setup(j => j.GenerateToken(user))
      .Returns(expectedResult);

    var sut = CreateSut();

    // Act
    var result = await sut.LoginAsync(dto);

    // Assert
    result.Should().BeSameAs(expectedResult);
    _repoMock.Verify(r => r.GetByEmailAsync(dto.Email), Times.Once);
    _passwordHashServiceMock.Verify(p => p.Verify(dto.Password, user.PasswordHash), Times.Once);
    _jwtTokenServiceMock.Verify(j => j.GenerateToken(user), Times.Once);
  }
}
