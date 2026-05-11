using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Wards;

public class WardService : IWardService
{
	private readonly IWardRepo _repo;
	private readonly IMunicipalityRepo _municipalityRepo;
	private readonly IValidator<CreateWardDTO> _createWardValidator;
	private readonly IValidator<UpdateWardDTO> _updateWardValidator;

	public WardService(
		IWardRepo repo,
		IMunicipalityRepo municipalityRepo,
		IValidator<CreateWardDTO> createWardValidator,
		IValidator<UpdateWardDTO> updateWardValidator)
	{
		_repo = repo;
		_municipalityRepo = municipalityRepo;
		_createWardValidator = createWardValidator;
		_updateWardValidator = updateWardValidator;
	}

	public async Task<WardDTO> CreateWardAsync(CreateWardDTO dto)
	{
		await _createWardValidator.EnsureValidOrThrowAsync(dto);

		var municipality = await _municipalityRepo.GetMunicipalityByIdAsync(dto.MunicipalityId)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), dto.MunicipalityId);

		var ward = await _repo.CreateWardAsync(new Ward
		{
			Id = Guid.NewGuid(),
			MunicipalityId = municipality.Id,
			Number = dto.Number,
			RepresentativeNameEn = dto.RepresentativeNameEn,
			RepresentativeNameNe = dto.RepresentativeNameNe,
			Email = dto.Email,
			PhoneNo = dto.PhoneNo
		});
		return new WardDTO
		{
			Id = ward.Id,
			MunicipalityId = ward.MunicipalityId,
			Number = ward.Number,
			RepresentativeNameEn = ward.RepresentativeNameEn,
			RepresentativeNameNe = ward.RepresentativeNameNe,
			Email = ward.Email,
			PhoneNo = ward.PhoneNo
		};
	}

	public async Task<List<WardDTO>> GetAllWardsAsync()
	{
		var wards = await _repo.GetAllWardsAsync();
		return wards.Select(w => new WardDTO
		{
			Id = w.Id,
			MunicipalityId = w.MunicipalityId,
			Number = w.Number,
			RepresentativeNameEn = w.RepresentativeNameEn,
			RepresentativeNameNe = w.RepresentativeNameNe,
			Email = w.Email,
			PhoneNo = w.PhoneNo

		}).ToList();
	}

	public async Task<List<WardDTO>> GetWardsByMunicipalityIdAsync(Guid municipalityId)
	{
		var municipality = await _municipalityRepo.GetMunicipalityByIdAsync(municipalityId)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), municipalityId);

		var wards = await _repo.GetWardsByMunicipalityIdAsync(municipality.Id);
		return wards.Select(w => new WardDTO
		{
			Id = w.Id,
			MunicipalityId = w.MunicipalityId,
			Number = w.Number,
			RepresentativeNameEn = w.RepresentativeNameEn,
			RepresentativeNameNe = w.RepresentativeNameNe,
			Email = w.Email,
			PhoneNo = w.PhoneNo

		}).ToList();
	}

	public async Task<WardDTO> GetWardByIdAsync(Guid id)
	{
		var ward = await _repo.GetWardByIdAsync(id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), id);
		return new WardDTO
		{
			Id = ward.Id,
			MunicipalityId = ward.MunicipalityId,
			Number = ward.Number,
			RepresentativeNameEn = ward.RepresentativeNameEn,
			RepresentativeNameNe = ward.RepresentativeNameNe,
			Email = ward.Email,
			PhoneNo = ward.PhoneNo
		};	
	}

	public async Task<WardDTO	> UpdateWardAsync(Guid id, UpdateWardDTO dto)
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
		// check ward already exists with the same number in the same municipality
		if (dto.Number.HasValue)
		{
			var existingWard = await _repo.GetWardByNumberAndMunicipalityIdAsync(dto.Number.Value, ward.MunicipalityId);
			if (existingWard != null && existingWard.Id != id)
			{
				throw new InvalidOperationException("A ward with the same number already exists in the specified municipality.");
			}
			ward.Number = dto.Number.Value;
		}

		if (dto.RepresentativeNameEn is not null)
			ward.RepresentativeNameEn = dto.RepresentativeNameEn;

		if (dto.RepresentativeNameNe is not null)
			ward.RepresentativeNameNe = dto.RepresentativeNameNe;
		if (dto.Email is not null)
			ward.Email = dto.Email;
		if (dto.PhoneNo is not null)
			ward.PhoneNo = dto.PhoneNo;


		var updatedWard = await _repo.UpdateWardAsync(ward);
		return new WardDTO
		{
			Id = updatedWard.Id,
			MunicipalityId = updatedWard.MunicipalityId,
			Number = updatedWard.Number,
			RepresentativeNameEn = updatedWard.RepresentativeNameEn,
			RepresentativeNameNe = updatedWard.RepresentativeNameNe,
			Email = updatedWard.Email,
			PhoneNo = updatedWard.PhoneNo
		};
	}

	public async Task DeleteWardAsync(Guid id)
	{
		var ward = await _repo.GetWardByIdAsync(id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), id);

		await _repo.DeleteWardAsync(ward.Id);
	}
}
