using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepo _repo;

    public FamilyService(IFamilyRepo repo)
    {
        _repo = repo;
    }

public async Task CreateFamilyAsync(CreateFamilyDto dto)
    {
        var family = new Family
        {
            Id = Guid.NewGuid(),
            SubmissionId = dto.SubmissionId,
            ResidenceHouseId = dto.ResidenceHouseId,
            ResidentTypeId = dto.ResidentTypeId
        };

        await _repo.CreateFamilyAsync(family);
    }

    public async Task<FamilyDto?> GetFamilyByIdAsync(Guid id)
    {
        var family = await _repo.GetFamilyByIdAsync(id);
        return family == null ? null : ToFamilyDto.ToDto(family);
    }

    
}