using FluentValidation;
using MIS.Application.Common.Validations;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Municipalities;

public class MunicipalityService : IMunicipalityService
{
	private readonly IMunicipalityRepo _repo;
	private readonly IValidator<CreateMunicipalityDTO> _createMunicipalityValidator;
	private readonly IValidator<UpdateMunicipalityDTO> _updateMunicipalityValidator;

	public MunicipalityService(
		IMunicipalityRepo repo,
		IValidator<CreateMunicipalityDTO> createMunicipalityValidator,
		IValidator<UpdateMunicipalityDTO> updateMunicipalityValidator)
	{
		_repo = repo;
		_createMunicipalityValidator = createMunicipalityValidator;
		_updateMunicipalityValidator = updateMunicipalityValidator;
	}

	public async Task<Municipality> CreateMunicipalityAsync(CreateMunicipalityDTO dto)
	{
		await _createMunicipalityValidator.EnsureValidOrThrowAsync(dto);

		return await _repo.CreateMunicipalityAsync(new Municipality
		{
			Id = Guid.NewGuid(),
			Code = dto.Code,
			NameEn = dto.NameEn,
			NameNe = dto.NameNe
		});
	}

	public async Task<List<Municipality>> GetAllMunicipalitiesAsync()
	{
		return await _repo.GetAllMunicipalitiesAsync();
	}

	public async Task<Municipality> GetMunicipalityByIdAsync(Guid id)
	{
		return await _repo.GetMunicipalityByIdAsync(id)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), id);
	}

	public async Task<Municipality> UpdateMunicipalityAsync(Guid id, UpdateMunicipalityDTO dto)
	{
		await _updateMunicipalityValidator.EnsureValidOrThrowAsync(dto);

		var municipality = await _repo.GetMunicipalityByIdAsync(id)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), id);

		if (!string.IsNullOrWhiteSpace(dto.Code))
			municipality.Code = dto.Code;

		if (!string.IsNullOrWhiteSpace(dto.NameEn))
			municipality.NameEn = dto.NameEn;

		if (!string.IsNullOrWhiteSpace(dto.NameNe))
			municipality.NameNe = dto.NameNe;

		return await _repo.UpdateMunicipalityAsync(municipality);
	}

	public async Task DeleteMunicipalityAsync(Guid id)
	{
		var municipality = await _repo.GetMunicipalityByIdAsync(id)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), id);

		await _repo.DeleteMunicipalityAsync(municipality.Id);
	}
}
