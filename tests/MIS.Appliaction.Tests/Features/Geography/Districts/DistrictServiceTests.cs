using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using MIS.Application.Features.Geography.Districts;
using MIS.Application.Features.Geography.Provinces;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Tests.Features.Geography.Districts;

public class DistrictServiceTests
{
  private readonly Mock<IDistrictRepo> _repoMock = new();
  private readonly Mock<IProvinceRepo> _provinceRepoMock = new();
  private readonly Mock<IValidator<CreateDistrictDTO>> _createValidatorMock = new();
  private readonly Mock<IValidator<UpdateDistrictDTO>> _updateValidatorMock = new();

  private DistrictService CreateSut()
  {
    return new DistrictService(
      _repoMock.Object,
      _provinceRepoMock.Object,
      _createValidatorMock.Object,
      _updateValidatorMock.Object);
  }

  [Fact]
  public async Task CreateDistrictAsync_WhenValidationFails_ShouldThrowDataValidationException()
  {
    var dto = new CreateDistrictDTO { ProvinceId = Guid.Empty, Code = "", NameEn = "", NameNe = "" };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Code", "District code is required") }));

    var sut = CreateSut();

    var act = async () => await sut.CreateDistrictAsync(dto);

    await act.Should().ThrowAsync<DataValidationException>();
    _repoMock.Verify(r => r.CreateDistrictAsync(It.IsAny<District>()), Times.Never);
  }

  [Fact]
  public async Task CreateDistrictAsync_WhenProvinceNotFound_ShouldThrowNotFoundException()
  {
    var dto = new CreateDistrictDTO { ProvinceId = Guid.NewGuid(), Code = "D1", NameEn = "Kathmandu", NameNe = "काठमाण्डौ" };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _provinceRepoMock
      .Setup(r => r.GetProvinceByIdAsync(dto.ProvinceId))
      .ReturnsAsync((Province?)null);

    var sut = CreateSut();

    var act = async () => await sut.CreateDistrictAsync(dto);

    await act.Should().ThrowAsync<NotFoundException>();
  }

  [Fact]
  public async Task CreateDistrictAsync_WhenCodeExists_ShouldThrowConflictException()
  {
    var dto = new CreateDistrictDTO { ProvinceId = Guid.NewGuid(), Code = "D1", NameEn = "Kathmandu", NameNe = "काठमाण्डौ" };
    var province = new Province { Id = dto.ProvinceId, Code = "P1", NameEn = "Koshi", NameNe = "कोशी" };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _provinceRepoMock
      .Setup(r => r.GetProvinceByIdAsync(dto.ProvinceId))
      .ReturnsAsync(province);

    _repoMock
      .Setup(r => r.GetByCodeAsync(dto.Code))
      .ReturnsAsync(new District { Id = Guid.NewGuid(), ProvinceId = dto.ProvinceId, Code = dto.Code, NameEn = "Existing", NameNe = "Existing" });

    var sut = CreateSut();

    var act = async () => await sut.CreateDistrictAsync(dto);

    await act.Should().ThrowAsync<ConflictException>();
  }

  [Fact]
  public async Task CreateDistrictAsync_WhenRequestIsValid_ShouldReturnCreatedDistrict()
  {
    var dto = new CreateDistrictDTO { ProvinceId = Guid.NewGuid(), Code = "D1", NameEn = "Kathmandu", NameNe = "काठमाण्डौ" };
    var province = new Province { Id = dto.ProvinceId, Code = "P1", NameEn = "Koshi", NameNe = "कोशी" };
    var district = new District { Id = Guid.NewGuid(), ProvinceId = dto.ProvinceId, Code = dto.Code, NameEn = dto.NameEn, NameNe = dto.NameNe };

    _createValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _provinceRepoMock.Setup(r => r.GetProvinceByIdAsync(dto.ProvinceId)).ReturnsAsync(province);
    _repoMock.Setup(r => r.GetByCodeAsync(dto.Code)).ReturnsAsync((District?)null);
    _repoMock.Setup(r => r.CreateDistrictAsync(It.IsAny<District>())).ReturnsAsync(district);

    var sut = CreateSut();

    var result = await sut.CreateDistrictAsync(dto);

    result.Id.Should().Be(district.Id);
    result.Code.Should().Be(dto.Code);
    result.ProvinceId.Should().Be(dto.ProvinceId);
  }

  [Fact]
  public async Task UpdateDistrictAsync_WhenDistrictNotFound_ShouldThrowNotFoundException()
  {
    var id = Guid.NewGuid();
    var dto = new UpdateDistrictDTO { NameEn = "Updated" };

    _updateValidatorMock
      .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
      .ReturnsAsync(new ValidationResult());

    _repoMock.Setup(r => r.GetDistrictByIdAsync(id)).ReturnsAsync((District?)null);

    var sut = CreateSut();

    var act = async () => await sut.UpdateDistrictAsync(id, dto);

    await act.Should().ThrowAsync<NotFoundException>();
  }
}
