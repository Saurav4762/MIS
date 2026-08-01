using System;
using System.Threading.Tasks;
using MIS.Application.Features.DataCollection.HouseholdInfo.Famiiles;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public interface IFamilyService
{
    Task CreateFamilyAsync(CreateFamilyDTO dto);
    
    Task<FamilyDTO?> GetFamilyByIdAsync(Guid id);
}