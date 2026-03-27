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
	private readonly IMunicipalityExcelParser _parser;

	public MunicipalityService(
		IMunicipalityRepo repo,
		IMunicipalityExcelParser parser,
		IValidator<CreateMunicipalityDTO> createMunicipalityValidator,
		IValidator<UpdateMunicipalityDTO> updateMunicipalityValidator)
	{
		_repo = repo;
		_parser = parser;
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
	public async Task<int> SeedMunicipalityAsync(IMunicipalitySeedDTO dto)
	{
		var municipalityDTOs = _parser.Parse(dto.file.Content);
		Dictionary<int, Dictionary<string, string[]>> errors = [];


		foreach (var (item, index) in municipalityDTOs.Select((i, j) => (i, j)))
		{
			var error = await _createMunicipalityValidator.EnsureValidOrToDictonary(item);
			if (error is not null)
			{
				errors.Add(index + 1, error);
			}
		}

		if (errors.Count != 0)
		{
			throw new DataValidationException(errors);
		}

		var municipalities = municipalityDTOs.Select(x => new Municipality
		{
			Id = Guid.NewGuid(),
			Code = x.Code,
			NameEn = x.NameEn,
			NameNe = x.NameNe
		}).ToList();
		return await _repo.BulkInsertAsync(municipalities);
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
