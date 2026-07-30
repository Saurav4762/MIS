using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Common.Interfaces;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public class FacilityService(IFacilityRepo facilityRepo, IOptionItemRepo optionItemRepo, IValidator<CreateFacilityDto> validator, IUnitOfWork unitOfWork) : IFacilityService
{
    private readonly IFacilityRepo _facilityRepo = facilityRepo;
    private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;
    private readonly IValidator<CreateFacilityDto> _validator = validator;

    private readonly IUnitOfWork _unitOfWork = unitOfWork;


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

    public async Task<FacilityDto> UpdateFacilityAsync(Guid familyId, UpdateFacilityDto facilityDto)
    {
        var facility = await _facilityRepo.GetFacilityByFamilyIdAsync(familyId) ?? throw new NotFoundException(nameof(Facility), nameof(Facility.FamilyId), familyId);
        await ValidateOptionExistsAsync(facilityDto.DrinkingWaterId);
        await ValidateOptionExistsAsync(facilityDto.ToiletTypeId);
        await ValidateOptionExistsAsync(facilityDto.ElectricityId);
        await ValidateOptionExistsAsync(facilityDto.AltLightId);
        await ValidateOptionExistsAsync(facilityDto.CookingFuelId);
        await ValidateOptionExistsAsync(facilityDto.StoveTypeId);


        facility.FamilyId = familyId;
        facility.DrinkingWaterId = facilityDto.DrinkingWaterId ?? facility.DrinkingWaterId;
        facility.ToiletTypeId = facilityDto.ToiletTypeId ?? facility.ToiletTypeId;
        facility.ElectricityId = facilityDto.ElectricityId ?? facility.ElectricityId;
        facility.AltLightId = facilityDto.AltLightId ?? facility.AltLightId;
        facility.CookingFuelId = facilityDto.CookingFuelId ?? facility.CookingFuelId;
        facility.StoveTypeId = facilityDto.StoveTypeId ?? facility.StoveTypeId;
        facility.MobilePhone = facilityDto.MobilePhone ?? facility.MobilePhone;
        facility.Radio = facilityDto.Radio ?? facility.Radio;
        facility.Television = facilityDto.Television ?? facility.Television;
        facility.Computer = facilityDto.Computer ?? facility.Computer;
        facility.Internet = facilityDto.Internet ?? facility.Internet;
        facility.Refrigerator = facilityDto.Refrigerator ?? facility.Refrigerator;
        facility.WashingMachine = facilityDto.WashingMachine ?? facility.WashingMachine;




        // var updatedFacility = await _facilityRepo.UpdateFacilityAsync(
        //     new Facility
        //     {
        //         FamilyId = familyId,
        //         DrinkingWaterId = facilityDto.DrinkingWaterId ?? facility.DrinkingWaterId,
        //         ToiletTypeId = facilityDto.ToiletTypeId ?? facility.ToiletTypeId,
        //         ElectricityId = facilityDto.ElectricityId ?? facility.ElectricityId,
        //         AltLightId = facilityDto.AltLightId ?? facility.AltLightId,
        //         CookingFuelId = facilityDto.CookingFuelId ?? facility.CookingFuelId,
        //         StoveTypeId = facilityDto.StoveTypeId ?? facility.StoveTypeId,
        //         MobilePhone = facilityDto.MobilePhone ?? facility.MobilePhone,
        //         Radio = facilityDto.Radio ?? facility.Radio,
        //         Television = facilityDto.Television ?? facility.Television,
        //         Computer = facilityDto.Computer ?? facility.Computer,
        //         Internet = facilityDto.Internet ?? facility.Internet,
        //         Refrigerator = facilityDto.Refrigerator ?? facility.Refrigerator,
        //         WashingMachine = facilityDto.WashingMachine ?? facility.WashingMachine
        //     }
        // );
        await _unitOfWork.SaveChangesAsync();
        return facility.ToFacilityDto();
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
