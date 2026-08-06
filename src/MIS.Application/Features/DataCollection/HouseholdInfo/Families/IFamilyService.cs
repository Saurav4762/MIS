using System;
using System.Threading.Tasks;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public interface IFamilyService
{
    Task CreateFamilyAsync(CreateFamilyDto dto);
    
    Task<FamilyDto?> GetFamilyByIdAsync(Guid id);
}