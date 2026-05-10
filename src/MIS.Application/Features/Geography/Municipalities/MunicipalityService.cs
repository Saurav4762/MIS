using System.Text.RegularExpressions;
using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Geography.Municipalities;

public class MunicipalityService : IMunicipalityService
{
	private readonly IMunicipalityRepo _repo;
	private readonly IValidator<CreateMunicipalityDTO> _createMunicipalityValidator;
	private readonly IValidator<UpdateMunicipalityDTO> _updateMunicipalityValidator;
	private readonly IValidator<ExcelMunicipalityDTO> _excelMunicipalityValidator;
	private readonly IMunicipalityExcelParser _parser;

	public MunicipalityService(
		IMunicipalityRepo repo,
		IMunicipalityExcelParser parser,
		IValidator<CreateMunicipalityDTO> createMunicipalityValidator,
		IValidator<UpdateMunicipalityDTO> updateMunicipalityValidator,
		IValidator<ExcelMunicipalityDTO> excelMunicipalityValidator)
	{
		_repo = repo;
		_parser = parser;
		_createMunicipalityValidator = createMunicipalityValidator;
		_updateMunicipalityValidator = updateMunicipalityValidator;
		_excelMunicipalityValidator = excelMunicipalityValidator;
	}

	public async Task<Municipality> CreateMunicipalityAsync(CreateMunicipalityDTO dto)
	{
		await _createMunicipalityValidator.EnsureValidOrThrowAsync(dto);
		var existingMunicipality = await _repo.GetByUniqueIdentifiersAsync(dto.Code, dto.PhoneNo, dto.Email);

		if (existingMunicipality is not null)
		{
			var errors = new Dictionary<string, string[]>();
			if (existingMunicipality.Code.ToLower() == dto.Code.ToLower())
				errors.Add(nameof(Municipality.Code), ["The field already exists"]);

			if (existingMunicipality.Email.ToLower() == dto.Email.ToLower())
				errors.Add(nameof(Municipality.Email), ["The field already exists"]);

			if (existingMunicipality.PhoneNo == dto.PhoneNo)
				errors.Add(nameof(Municipality.PhoneNo), ["The field already exists"]);

			throw new ConflictException(errors);
		}


		return await _repo.CreateMunicipalityAsync(new Municipality
		{
			Id = Guid.NewGuid(),
			Code = dto.Code,
			NameEn = dto.NameEn,
			NameNe = dto.NameNe,
			Email = dto.Email,
			HeadExecutiveNameEn = dto.HeadExecutiveNameEn,
			HeadExecutiveNameNe = dto.HeadExecutiveNameNe,
			PhoneNo = dto.PhoneNo,
			Website = dto.Website
		});
	}
	public async Task<int> SeedMunicipalityAsync(IMunicipalitySeedDTO dto)
	{
		var municipalityDTOs = _parser.Parse(dto.file.Content);
		Dictionary<int, Dictionary<string, string[]>> errors = [];
		var hasSeenCode = new HashSet<string>();

		foreach (var item in municipalityDTOs)
		{
			var validationResult = _excelMunicipalityValidator.Validate(item);
			if (!validationResult.IsValid)
			{
				errors[item.RowNumber] = validationResult.Errors
					.GroupBy(x => x.PropertyName)
					.ToDictionary(
						group => group.Key,
						group => group.Select(x => x.ErrorMessage).ToArray()
					);
				continue;
			}

			if (!hasSeenCode.Add(item.Code))
			{
				if (errors.TryGetValue(item.RowNumber, out var existingRowError))
				{
					existingRowError.Add(nameof(item.Code), [$"provided code {item.Code} is a duplicate"]);
				}
				else
					errors.Add(item.RowNumber, new Dictionary<string, string[]>
				{
					{
						nameof(item.Code), [$"provided code {item.Code} is a duplicate"]
					}
				});
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
			NameNe = x.NameNe,
			HeadExecutiveNameEn = x.HeadExecutiveNameEn,
			HeadExecutiveNameNe = x.HeadExecutiveNameNe,
			Email = x.Email,
			PhoneNo = x.PhoneNo,
			Website = x.Website
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

		if (!string.IsNullOrWhiteSpace(dto.HeadExecutiveNameEn))
			municipality.HeadExecutiveNameEn = dto.HeadExecutiveNameEn;

		if (!string.IsNullOrWhiteSpace(dto.HeadExecutiveNameNe))
			municipality.HeadExecutiveNameNe = dto.HeadExecutiveNameNe;

		if (!string.IsNullOrWhiteSpace(dto.PhoneNo))
			municipality.PhoneNo = dto.PhoneNo;

		if (!string.IsNullOrWhiteSpace(dto.Email))
			municipality.Email = dto.Email;

		if (!string.IsNullOrWhiteSpace(dto.Website))
			municipality.Website = dto.Website;

		return await _repo.UpdateMunicipalityAsync(municipality);
	}

	public async Task DeleteMunicipalityAsync(Guid id)
	{
		var municipality = await _repo.GetMunicipalityByIdAsync(id)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), id);

		await _repo.DeleteMunicipalityAsync(municipality.Id);
	}

  public Task<List<Municipality>> SearchMunicipalitiesAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		return _repo.SearchMunicipalitiesAsync(searchQuery, searchBy, maxResults, pageNumber);
	}
}
