using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo;

public interface IMemberRepo
{
    Task<Member> CreateMemberAsync(Member member);
    Task<Member?> GetMemberByIdAsync(Guid id);
    Task<List<Member>> GetMembersByFamilyIdAsync(Guid familyId);
    Task<Member> UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Guid id);
}
