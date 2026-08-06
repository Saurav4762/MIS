using System;
using System.Threading.Tasks;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public interface IFamilyRepo
{
    Task CreateFamilyAsync(Family family);
    
    Task<Family?> GetFamilyByIdAsync(Guid id);
    
}