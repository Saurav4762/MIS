using System;
using System.Threading.Tasks;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo;

public interface IFamilyService
{
    Task CreateFamilyAsync(CreateFamilyDTO dto);
    
    Task<FamilyDTO?> GetFamilyByIdAsync(Guid id);
}