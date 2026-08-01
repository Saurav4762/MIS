using System;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Famiiles;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepo _repo;

    public FamilyService(IFamilyRepo repo)
    {
        _repo = repo;
    }

    public async Task CreateFamilyAsync(CreateFamilyDTO dto)
    {
       //validated externally (FluentValidation) or add checks here
       var family = new Family
       {
           Id = Guid.NewGuid(),
           SubmissionId = dto.SubmissionId,
           ResidenceHouseId = dto.ResidenceHouseId,
           ResidentTypeId = dto.ResidentTypeId
       };
       
       //map members
       foreach (var m in dto.Members)
       {
           family.Members.Add(ToMemberDto.ToEntity(m, family.Id));
       }
       await _repo.CreateFamilyAsync(family);

    }

    public async Task<FamilyDTO?> GetFamilyByIdAsync(Guid id)
    {
        var family = await _repo.GetFamilyByIdAsync(id);
        return family == null ? null : ToFamilyDto.ToDto(family);
    }
}