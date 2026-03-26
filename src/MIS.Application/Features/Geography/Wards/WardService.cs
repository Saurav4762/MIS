using FluentValidation;
using MIS.Application.Common.Validations;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Wards;

public class WardService : IWardService
{
	private readonly IWarrdRepo _repo;
	private readonly IMunicipalityRepo _municipalityRepo;
	private readonly IValidator<CreateWardDTO> _createWardValidator;
	private readonly IValidator<UpdateWardDTO> _updateWardValidator;

	public WardService(
		IWarrdRepo repo,
		IMunicipalityRepo municipalityRepo,
		IValidator<CreateWardDTO> createWardValidator,
		IValidator<UpdateWardDTO> updateWardValidator)
	{
		_repo = repo;
		_municipalityRepo = municipalityRepo;
		_createWardValidator = createWardValidator;
		_updateWardValidator = updateWardValidator;
	}

	public async Task<Ward> CreateWardAsync(CreateWardDTO dto)
	{
		await _createWardValidator.EnsureValidOrThrowAsync(dto);

		var municipality = await _municipalityRepo.GetMunicipalityByIdAsync(dto.MunicipalityId)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), dto.MunicipalityId);

		return await _repo.CreateWardAsync(new Ward
		{
			Id = Guid.NewGuid(),
			MunicipalityId = municipality.Id,
			Code = dto.Code,
			Name = dto.Name
		});
	}

	public async Task<List<Ward>> GetAllWardsAsync()
	{
		return await _repo.GetAllWardsAsync();
	}

	public async Task<List<Ward>> GetWardsByMunicipalityIdAsync(Guid municipalityId)
	{
		var municipality = await _municipalityRepo.GetMunicipalityByIdAsync(municipalityId)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), municipalityId);

		return await _repo.GetWardsByMunicipalityIdAsync(municipality.Id);
	}

	public async Task<Ward> GetWardByIdAsync(Guid id)
	{
		return await _repo.GetWardByIdAsync(id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), id);
	}

	public async Task<Ward> UpdateWardAsync(Guid id, UpdateWardDTO dto)
	{
		await _updateWardValidator.EnsureValidOrThrowAsync(dto);

		var ward = await _repo.GetWardByIdAsync(id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), id);

		if (dto.MunicipalityId.HasValue)
		{
			var municipality = await _municipalityRepo.GetMunicipalityByIdAsync(dto.MunicipalityId.Value)
				?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), dto.MunicipalityId.Value);
			ward.MunicipalityId = municipality.Id;
		}

		if (!string.IsNullOrWhiteSpace(dto.Code))
			ward.Code = dto.Code;

		if (!string.IsNullOrWhiteSpace(dto.Name))
			ward.Name = dto.Name;

		return await _repo.UpdateWardAsync(ward);
	}

	public async Task DeleteWardAsync(Guid id)
	{
		var ward = await _repo.GetWardByIdAsync(id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), id);

		await _repo.DeleteWardAsync(ward.Id);
	}
}
