using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Provinces;

public class ProvinceService : IProvinceService
{
	private readonly IProvinceRepo _repo;
	private readonly IValidator<CreateProvinceDTO> _createProvinceValidator;
	private readonly IValidator<UpdateProvinceDTO> _updateProvinceValidator;

	public ProvinceService(
		IProvinceRepo repo,
		IValidator<CreateProvinceDTO> createProvinceValidator,
		IValidator<UpdateProvinceDTO> updateProvinceValidator)
	{
		_repo = repo;
		_createProvinceValidator = createProvinceValidator;
		_updateProvinceValidator = updateProvinceValidator;
	}

	public async Task<ProvinceDTO> CreateProvinceAsync(CreateProvinceDTO dto)
	{
		await _createProvinceValidator.EnsureValidOrThrowAsync(dto);

		var existingProvince = await _repo.GetByCodeAsync(dto.Code);
		if (existingProvince is not null)
		{
			throw new ConflictException(new Dictionary<string, string[]>
			{
				{ nameof(Province.Code), ["The field already exists"] }
			});
		}

		var province = await _repo.CreateProvinceAsync(new Province
		{
			Id = Guid.NewGuid(),
			Code = dto.Code,
			NameEn = dto.NameEn,
			NameNe = dto.NameNe
		});

		return ToDTO(province);
	}

	public async Task<List<ProvinceDTO>> GetAllProvincesAsync()
	{
		var provinces = await _repo.GetAllProvincesAsync();
		return provinces.Select(ToDTO).ToList();
	}

	public async Task<ProvinceDTO> GetProvinceByIdAsync(Guid id)
	{
		var province = await _repo.GetProvinceByIdAsync(id)
			?? throw new NotFoundException(nameof(Province), nameof(Province.Id), id);

		return ToDTO(province);
	}

	public async Task<ProvinceDTO> UpdateProvinceAsync(Guid id, UpdateProvinceDTO dto)
	{
		await _updateProvinceValidator.EnsureValidOrThrowAsync(dto);

		var province = await _repo.GetProvinceByIdAsync(id)
			?? throw new NotFoundException(nameof(Province), nameof(Province.Id), id);

		if (!string.IsNullOrWhiteSpace(dto.Code))
		{
			var existingProvince = await _repo.GetByCodeAsync(dto.Code);
			if (existingProvince is not null && existingProvince.Id != id)
			{
				throw new ConflictException(new Dictionary<string, string[]>
				{
					{ nameof(Province.Code), ["The field already exists"] }
				});
			}

			province.Code = dto.Code;
		}

		if (!string.IsNullOrWhiteSpace(dto.NameEn))
			province.NameEn = dto.NameEn;

		if (!string.IsNullOrWhiteSpace(dto.NameNe))
			province.NameNe = dto.NameNe;

		var updatedProvince = await _repo.UpdateProvinceAsync(province);
		return ToDTO(updatedProvince);
	}

	public async Task DeleteProvinceAsync(Guid id)
	{
		var province = await _repo.GetProvinceByIdAsync(id)
			?? throw new NotFoundException(nameof(Province), nameof(Province.Id), id);

		await _repo.DeleteProvinceAsync(province.Id);
	}

	public async Task<List<ProvinceDTO>> SearchProvincesAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		var provinces = await _repo.SearchProvincesAsync(searchQuery, searchBy, maxResults, pageNumber);
		return provinces.Select(ToDTO).ToList();
	}

	private static ProvinceDTO ToDTO(Province province)
	{
		return new ProvinceDTO
		{
			Id = province.Id,
			Code = province.Code,
			NameEn = province.NameEn,
			NameNe = province.NameNe
		};
	}
}
