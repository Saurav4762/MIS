using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public interface IMemberService
{
    Task<MemberDto> CreateMemberAsync(CreateMemberDTO dto);
    Task<MemberDto> GetMemberByIdAsync(Guid id);
    Task<List<MemberDto>> GetMembersByFamilyIdAsync(Guid familyId);
    Task<MemberDto> UpdateMemberAsync(Guid id, UpdateMemberDTO dto);
    Task DeleteMemberAsync(Guid id);
}
