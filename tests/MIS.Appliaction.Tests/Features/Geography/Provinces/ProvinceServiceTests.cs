using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using MIS.Application.Features.Geography.Provinces;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Tests.Features.Geography.Provinces;

public class ProvinceServiceTests
{
  private readonly Mock<IProvinceRepo> _repoMock = new();
  private readonly Mock<IValidator<CreateProvinceDTO>> _createValidatorMock = new();
  private readonly Mock<IValidator<UpdateProvinceDTO>> _updateValidatorMock = new();

  private ProvinceService CreateSut()
  {
    return new ProvinceService(
      _repoMock.Object,
      _createValidatorMock.Object,
      _updateValidatorMock.Object);
  }

  [Fact]
  public async Task CreateProvinceAsync_WhenValidationFails_ShouldThrowDataValidationException()
  {
    var dto = new CreateProvinceDTO { Code = "", NameEn = "", NameNe = "" };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Code", "Province code is required") }));

    var sut = CreateSut();

    var act = async () => await sut.CreateProvinceAsync(dto);

    await act.Should().ThrowAsync<DataValidationException>();
    _repoMock.Verify(r => r.CreateProvinceAsync(It.IsAny<Province>()), Times.Never);
  }

  [Fact]
  public async Task CreateProvinceAsync_WhenCodeAlreadyExists_ShouldThrowConflictException()
  {
    var dto = new CreateProvinceDTO { Code = "P1", NameEn = "Koshi", NameNe = "कोशी" };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock
      .Setup(r => r.GetByCodeAsync(dto.Code))
      .ReturnsAsync(new Province { Id = Guid.NewGuid(), Code = dto.Code, NameEn = "Existing", NameNe = "Existing" });

    var sut = CreateSut();

    var act = async () => await sut.CreateProvinceAsync(dto);

    await act.Should().ThrowAsync<ConflictException>();
  }

  [Fact]
  public async Task CreateProvinceAsync_WhenRequestIsValid_ShouldReturnProvinceDto()
  {
    var dto = new CreateProvinceDTO { Code = "P2", NameEn = "Madhesh", NameNe = "मधेश" };
    var entity = new Province { Id = Guid.NewGuid(), Code = dto.Code, NameEn = dto.NameEn, NameNe = dto.NameNe };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock.Setup(r => r.GetByCodeAsync(dto.Code)).ReturnsAsync((Province?)null);
    _repoMock.Setup(r => r.CreateProvinceAsync(It.IsAny<Province>())).ReturnsAsync(entity);

    var sut = CreateSut();

    var result = await sut.CreateProvinceAsync(dto);

    result.Id.Should().Be(entity.Id);
    result.Code.Should().Be(entity.Code);
    result.NameEn.Should().Be(entity.NameEn);
    result.NameNe.Should().Be(entity.NameNe);
  }

  [Fact]
  public async Task GetProvinceByIdAsync_WhenNotFound_ShouldThrowNotFoundException()
  {
    var id = Guid.NewGuid();
    _repoMock.Setup(r => r.GetProvinceByIdAsync(id)).ReturnsAsync((Province?)null);

    var sut = CreateSut();

    var act = async () => await sut.GetProvinceByIdAsync(id);

    await act.Should().ThrowAsync<NotFoundException>();
  }

  [Fact]
  public async Task SearchProvincesAsync_ShouldMapToProvinceDtoList()
  {
    var provinces = new List<Province>
    {
      new() { Id = Guid.NewGuid(), Code = "P1", NameEn = "Koshi", NameNe = "कोशी" },
      new() { Id = Guid.NewGuid(), Code = "P2", NameEn = "Madhesh", NameNe = "मधेश" }
    };

    _repoMock
      .Setup(r => r.SearchProvincesAsync("p", null, 10, 1))
      .ReturnsAsync(provinces);

    var sut = CreateSut();

    var result = await sut.SearchProvincesAsync("p");

    result.Should().HaveCount(2);
    result.Select(x => x.Code).Should().Contain(["P1", "P2"]);
  }
}
