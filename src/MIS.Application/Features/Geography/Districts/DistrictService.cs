using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Geography.Provinces;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Districts;

public class DistrictService : IDistrictService
{
	private readonly IDistrictRepo _repo;
	private readonly IProvinceRepo _provinceRepo;
	private readonly IValidator<CreateDistrictDTO> _createDistrictValidator;
	private readonly IValidator<UpdateDistrictDTO> _updateDistrictValidator;

	public DistrictService(
		IDistrictRepo repo,
		IProvinceRepo provinceRepo,
		IValidator<CreateDistrictDTO> createDistrictValidator,
		IValidator<UpdateDistrictDTO> updateDistrictValidator)
	{
		_repo = repo;
		_provinceRepo = provinceRepo;
		_createDistrictValidator = createDistrictValidator;
		_updateDistrictValidator = updateDistrictValidator;
	}

	public async Task<DistrictDTO> CreateDistrictAsync(CreateDistrictDTO dto)
	{
		Console.WriteLine("......");
		Console.WriteLine("......");
		Console.WriteLine("......");
		await _createDistrictValidator.EnsureValidOrThrowAsync(dto);
		Console.WriteLine(dto.ProvinceId);
		Console.WriteLine("......");
		Console.WriteLine("......");
		Console.WriteLine("......");
		var province = await _provinceRepo.GetProvinceByIdAsync(dto.ProvinceId)
		               ?? throw new NotFoundException(nameof(Province), nameof(Province.Id), dto.ProvinceId);

		var existingDistrict = await _repo.GetByCodeAsync(dto.Code);
		if (existingDistrict is not null)
		{
			throw new ConflictException(new Dictionary<string, string[]>
			{
				{ nameof(District.Code), ["The field already exists"] }
			});
		}

		var district = await _repo.CreateDistrictAsync(new District
		{
			Id = Guid.NewGuid(),
			ProvinceId = province.Id,
			Code = dto.Code,
			NameEn = dto.NameEn,
			NameNe = dto.NameNe
		});
		return district.ToDistrictDTO();
	}

	public async Task<List<DistrictDTO>> GetAllDistrictsAsync()
	{
		var districts = await _repo.GetAllDistrictsAsync();
		return [.. districts.Select(d => d.ToDistrictDTO())];
	}

	public async Task<DistrictDTO> GetDistrictByIdAsync(Guid id)
	{
		var district = await _repo.GetDistrictByIdAsync(id)
			?? throw new NotFoundException(nameof(District), nameof(District.Id), id);
		return district.ToDistrictDTO();
	}

	public async Task<List<DistrictDTO>> GetDistrictsByProvinceIdAsync(Guid provinceId)
	{
		var districts = await _repo.GetDistrictsByProvinceIdAsync(provinceId);
		return [.. districts.Select(d => d.ToDistrictDTO())];
	}

	public async Task<DistrictDTO> UpdateDistrictAsync(Guid id, UpdateDistrictDTO dto)
	{
		await _updateDistrictValidator.EnsureValidOrThrowAsync(dto);

		var district = await _repo.GetDistrictByIdAsync(id)
			?? throw new NotFoundException(nameof(District), nameof(District.Id), id);

		if (dto.ProvinceId.HasValue)
		{
			var province = await _provinceRepo.GetProvinceByIdAsync(dto.ProvinceId.Value)
				?? throw new NotFoundException(nameof(Province), nameof(Province.Id), dto.ProvinceId.Value);
			district.ProvinceId = province.Id;
		}

		if (!string.IsNullOrWhiteSpace(dto.Code))
		{
			var existingDistrict = await _repo.GetByCodeAsync(dto.Code);
			if (existingDistrict is not null && existingDistrict.Id != id)
			{
				throw new ConflictException(new Dictionary<string, string[]>
				{
					{ nameof(District.Code), ["The field already exists"] }
				});
			}

			district.Code = dto.Code;
		}

		if (!string.IsNullOrWhiteSpace(dto.NameEn))
			district.NameEn = dto.NameEn;

		if (!string.IsNullOrWhiteSpace(dto.NameNe))
			district.NameNe = dto.NameNe;

		var updatedDistrict = await _repo.UpdateDistrictAsync(district);
		return updatedDistrict.ToDistrictDTO();
	}

	public async Task DeleteDistrictAsync(Guid id)
	{
		var district = await _repo.GetDistrictByIdAsync(id)
			?? throw new NotFoundException(nameof(District), nameof(District.Id), id);

		await _repo.DeleteDistrictAsync(district.Id);
	}

	public async Task<List<DistrictDTO>> SearchDistrictsAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		var districts = await _repo.SearchDistrictsAsync(searchQuery, searchBy, maxResults, pageNumber);
		return [.. districts.Select(d => d.ToDistrictDTO())];
	}
}
