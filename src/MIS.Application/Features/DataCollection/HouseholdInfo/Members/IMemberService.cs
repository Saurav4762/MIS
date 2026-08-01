using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public interface IMemberService
{
    Task<MemberDto> CreateMemberAsync(CreateMemberDto dto);
    Task<MemberDto> GetMemberByIdAsync(Guid id);
    Task<List<MemberDto>> GetMembersByFamilyIdAsync(Guid familyId);
    Task<MemberDto> UpdateMemberAsync(Guid id, UpdateMemberDto dto);
    Task DeleteMemberAsync(Guid id);
}
