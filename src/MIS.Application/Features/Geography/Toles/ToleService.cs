using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Geography.Wards;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Toles;

public class ToleService : IToleService
{
	private readonly IToleRepo _repo;
	private readonly IWardRepo _wardRepo;
	private readonly IValidator<CreateToleDTO> _createToleValidator;
	private readonly IValidator<UpdateToleDTO> _updateToleValidator;

	public ToleService(
		IToleRepo repo,
		IWardRepo wardRepo,
		IValidator<CreateToleDTO> createToleValidator,
		IValidator<UpdateToleDTO> updateToleValidator)
	{
		_repo = repo;
		_wardRepo = wardRepo;
		_createToleValidator = createToleValidator;
		_updateToleValidator = updateToleValidator;
	}

	public async Task<ToleDTO> CreateToleAsync(CreateToleDTO dto)
	{
		await _createToleValidator.EnsureValidOrThrowAsync(dto);

		var ward = await _wardRepo.GetWardByIdAsync(dto.WardId)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), dto.WardId);

		var tole = await _repo.CreateToleAsync(new Tole
		{
			Id = Guid.NewGuid(),
			WardId = ward.Id,
			Code = dto.Code,
			Name = dto.Name
		});
		return tole.ToToleDTO();
	}

	public async Task<List<ToleDTO>> GetAllTolesAsync()
	{
		var toles = await _repo.GetAllTolesAsync();
		return toles.Select(t => t.ToToleDTO()).ToList();
	}

	public async Task<List<ToleDTO>> GetTolesByWardIdAsync(Guid wardId)
	{
		var ward = await _wardRepo.GetWardByIdAsync(wardId)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), wardId);

		var toles = await _repo.GetTolesByWardIdAsync(ward.Id);
		return toles.Select(t => t.ToToleDTO()).ToList();
	}

	public async Task<ToleDTO> GetToleByIdAsync(Guid id)
	{
		var tole = await _repo.GetToleByIdAsync(id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), id);
		return tole.ToToleDTO();
	}

	public async Task<ToleDTO> UpdateToleAsync(Guid id, UpdateToleDTO dto)
	{
		await _updateToleValidator.EnsureValidOrThrowAsync(dto);

		var tole = await _repo.GetToleByIdAsync(id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), id);

		if (dto.WardId.HasValue)
		{
			var ward = await _wardRepo.GetWardByIdAsync(dto.WardId.Value)
				?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), dto.WardId.Value);
			tole.WardId = ward.Id;
		}

		if (!string.IsNullOrWhiteSpace(dto.Code))
			tole.Code = dto.Code;

		if (!string.IsNullOrWhiteSpace(dto.Name))
			tole.Name = dto.Name;

		var updatedTole = await _repo.UpdateToleAsync(tole);
		return updatedTole.ToToleDTO();
	}

	public async Task DeleteToleAsync(Guid id)
	{
		var tole = await _repo.GetToleByIdAsync(id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), id);

		await _repo.DeleteToleAsync(tole.Id);
	}
}
