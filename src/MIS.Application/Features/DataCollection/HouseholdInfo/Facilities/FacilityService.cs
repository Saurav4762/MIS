using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public class FacilityService(IFacilityRepo facilityRepo, IOptionItemRepo optionItemRepo, IValidator<CreateFacilityDto> validator) : IFacilityService
{
    private readonly IFacilityRepo _facilityRepo = facilityRepo;
    private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;
    private readonly IValidator<CreateFacilityDto> _validator = validator;

    public async Task<FacilityDto> CreateFacilityAsync(CreateFacilityDto createFacilityDto)
    {
        await _validator.EnsureValidOrThrowAsync(createFacilityDto);

        await ValidateOptionExistsAsync(createFacilityDto.DrinkingWaterId);
        await ValidateOptionExistsAsync(createFacilityDto.ToiletTypeId);
        await ValidateOptionExistsAsync(createFacilityDto.ElectricityId);
        await ValidateOptionExistsAsync(createFacilityDto.AltLightId);
        await ValidateOptionExistsAsync(createFacilityDto.CookingFuelId);
        await ValidateOptionExistsAsync(createFacilityDto.StoveTypeId);

        var facility = new Facility
        {
            FamilyId = createFacilityDto.FamilyId,
            DrinkingWaterId = createFacilityDto.DrinkingWaterId,
            ToiletTypeId = createFacilityDto.ToiletTypeId,
            ElectricityId = createFacilityDto.ElectricityId,
            AltLightId = createFacilityDto.AltLightId,
            CookingFuelId = createFacilityDto.CookingFuelId,
            StoveTypeId = createFacilityDto.StoveTypeId,
            MobilePhone = createFacilityDto.MobilePhone,
            Radio = createFacilityDto.Radio,
            Television = createFacilityDto.Television,
            Computer = createFacilityDto.Computer,
            Internet = createFacilityDto.Internet,
            Refrigerator = createFacilityDto.Refrigerator,
            WashingMachine = createFacilityDto.WashingMachine
        };

        var createdFacility = await _facilityRepo.CreateFacilityAsync(facility);
        return createdFacility.ToFacilityDto();
    }

    public async Task<FacilityDto> GetFacilityByFamilyIdAsync(Guid familyId)
    {
        var facility = await _facilityRepo.GetFacilityByFamilyIdAsync(familyId);
        return facility?.ToFacilityDto() ?? throw new NotFoundException(nameof(Family), nameof(Facility.FamilyId), familyId);
    }

    public async Task<FacilityDto> UpdateFacilityAsync(Guid familyId, FacilityDto facilityDto)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateOptionExistsAsync(Guid? optionId)
    {
        if (!optionId.HasValue)
        {
            return;
        }

        var exists = await _optionItemRepo.CheckIfOptionItemExistsAsync(optionId.Value);
        if (!exists)
        {
            throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), optionId.Value);
        }
    }
}
