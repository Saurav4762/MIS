using FluentValidation;
using MIS.Application.Common.Validations;
using MIS.Application.Features.Geography.Wards;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Toles;

public class ToleService : IToleService
{
	private readonly IToleRepo _repo;
	private readonly IWarrdRepo _wardRepo;
	private readonly IValidator<CreateToleDTO> _createToleValidator;
	private readonly IValidator<UpdateToleDTO> _updateToleValidator;

	public ToleService(
		IToleRepo repo,
		IWarrdRepo wardRepo,
		IValidator<CreateToleDTO> createToleValidator,
		IValidator<UpdateToleDTO> updateToleValidator)
	{
		_repo = repo;
		_wardRepo = wardRepo;
		_createToleValidator = createToleValidator;
		_updateToleValidator = updateToleValidator;
	}

	public async Task<Tole> CreateToleAsync(CreateToleDTO dto)
	{
		await _createToleValidator.EnsureValidOrThrowAsync(dto);

		var ward = await _wardRepo.GetWardByIdAsync(dto.WardId)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), dto.WardId);

		return await _repo.CreateToleAsync(new Tole
		{
			Id = Guid.NewGuid(),
			WardId = ward.Id,
			Code = dto.Code,
			Name = dto.Name
		});
	}

	public async Task<List<Tole>> GetAllTolesAsync()
	{
		return await _repo.GetAllTolesAsync();
	}

	public async Task<List<Tole>> GetTolesByWardIdAsync(Guid wardId)
	{
		var ward = await _wardRepo.GetWardByIdAsync(wardId)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), wardId);

		return await _repo.GetTolesByWardIdAsync(ward.Id);
	}

	public async Task<Tole> GetToleByIdAsync(Guid id)
	{
		return await _repo.GetToleByIdAsync(id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), id);
	}

	public async Task<Tole> UpdateToleAsync(Guid id, UpdateToleDTO dto)
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

		return await _repo.UpdateToleAsync(tole);
	}

	public async Task DeleteToleAsync(Guid id)
	{
		var tole = await _repo.GetToleByIdAsync(id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), id);

		await _repo.DeleteToleAsync(tole.Id);
	}
}
