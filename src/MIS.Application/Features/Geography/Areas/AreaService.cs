using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Geography.Districts;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Areas;

public class AreaService : IAreaService
{
	private readonly IAreaRepo _repo;
	private readonly IDistrictRepo _districtRepo;
	private readonly IValidator<CreateAreaDTO> _createAreaValidator;
	private readonly IValidator<UpdateAreaDTO> _updateAreaValidator;

	public AreaService(
		IAreaRepo repo,
		IDistrictRepo districtRepo,
		IValidator<CreateAreaDTO> createAreaValidator,
		IValidator<UpdateAreaDTO> updateAreaValidator)
	{
		_repo = repo;
		_districtRepo = districtRepo;
		_createAreaValidator = createAreaValidator;
		_updateAreaValidator = updateAreaValidator;
	}

	public async Task<AreaDTO> CreateAreaAsync(CreateAreaDTO dto)
	{
		await _createAreaValidator.EnsureValidOrThrowAsync(dto);

		var district = await _districtRepo.GetDistrictByIdAsync(dto.DistrictId)
			?? throw new NotFoundException(nameof(District), nameof(District.Id), dto.DistrictId);

		var existingArea = await _repo.GetByCodeAsync(dto.Code);
		if (existingArea is not null)
		{
			throw new ConflictException(new Dictionary<string, string[]>
			{
				{ nameof(Area.Code), ["The field already exists"] }
			});
		}

		var area = await _repo.CreateAreaAsync(new Area
		{
			Id = Guid.NewGuid(),
			DistrictId = district.Id,
			Code = dto.Code,
			NameEn = dto.NameEn,
			NameNe = dto.NameNe
		});
		return area.ToAreaDTO();
	}

	public async Task<List<AreaDTO>> GetAllAreasAsync()
	{
		var areas = await _repo.GetAllAreasAsync();
		return [.. areas.Select(a => a.ToAreaDTO())];
	}

	public async Task<AreaDTO> GetAreaByIdAsync(Guid id)
	{
		var area = await _repo.GetAreaByIdAsync(id)
			?? throw new NotFoundException(nameof(Area), nameof(Area.Id), id);
		return area.ToAreaDTO();
	}

	public async Task<List<AreaDTO>> GetAreasByDistrictIdAsync(Guid districtId)
	{
		var areas = await _repo.GetAreasByDistrictIdAsync(districtId);
		return [.. areas.Select(a => a.ToAreaDTO())];
	}

	public async Task<AreaDTO> UpdateAreaAsync(Guid id, UpdateAreaDTO dto)
	{
		await _updateAreaValidator.EnsureValidOrThrowAsync(dto);

		var area = await _repo.GetAreaByIdAsync(id)
			?? throw new NotFoundException(nameof(Area), nameof(Area.Id), id);

		if (dto.DistrictId.HasValue)
		{
			var district = await _districtRepo.GetDistrictByIdAsync(dto.DistrictId.Value)
				?? throw new NotFoundException(nameof(District), nameof(District.Id), dto.DistrictId.Value);
			area.DistrictId = district.Id;
		}

		if (!string.IsNullOrWhiteSpace(dto.Code))
		{
			var existingArea = await _repo.GetByCodeAsync(dto.Code);
			if (existingArea is not null && existingArea.Id != id)
			{
				throw new ConflictException(new Dictionary<string, string[]>
				{
					{ nameof(Area.Code), ["The field already exists"] }
				});
			}

			area.Code = dto.Code;
		}

		if (!string.IsNullOrWhiteSpace(dto.NameEn))
			area.NameEn = dto.NameEn;

		if (!string.IsNullOrWhiteSpace(dto.NameNe))
			area.NameNe = dto.NameNe;

		var updatedArea = await _repo.UpdateAreaAsync(area);
		return updatedArea.ToAreaDTO();
	}

	public async Task DeleteAreaAsync(Guid id)
	{
		var area = await _repo.GetAreaByIdAsync(id)
			?? throw new NotFoundException(nameof(Area), nameof(Area.Id), id);

		await _repo.DeleteAreaAsync(area.Id);
	}

	public async Task<List<AreaDTO>> SearchAreasAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		var areas = await _repo.SearchAreasAsync(searchQuery, searchBy, maxResults, pageNumber);
		return [.. areas.Select(a => a.ToAreaDTO())];
	}
}
