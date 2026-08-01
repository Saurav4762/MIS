using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Families;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public class MemberService : IMemberService
{
    private readonly IMemberRepo _memberRepo;
    private readonly IFamilyRepo _familyRepo;
    private readonly IValidator<CreateMemberDto> _createValidator;
    private readonly IValidator<UpdateMemberDto> _updateValidator;

    public MemberService(
        IMemberRepo memberRepo,
        IFamilyRepo familyRepo,
        IValidator<CreateMemberDto> createValidator,
        IValidator<UpdateMemberDto> updateValidator)
    {
        _memberRepo = memberRepo;
        _familyRepo = familyRepo;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<MemberDto> CreateMemberAsync(CreateMemberDto dto)
    {
        await _createValidator.EnsureValidOrThrowAsync(dto);

        _ = await _familyRepo.GetFamilyByIdAsync(dto.FamilyId)
            ?? throw new NotFoundException(nameof(Family), nameof(Family.Id), dto.FamilyId);

        var member = await _memberRepo.CreateMemberAsync(ToMemberDto.ToEntity(dto, dto.FamilyId));
        return ToMemberDto.ToDto(member);
    }

    public async Task<MemberDto> GetMemberByIdAsync(Guid id)
    {
        var member = await _memberRepo.GetMemberByIdAsync(id)
            ?? throw new NotFoundException(nameof(Member), nameof(Member.Id), id);

        return ToMemberDto.ToDto(member);
    }

    public async Task<List<MemberDto>> GetMembersByFamilyIdAsync(Guid familyId)
    {
        _ = await _familyRepo.GetFamilyByIdAsync(familyId)
            ?? throw new NotFoundException(nameof(Family), nameof(Family.Id), familyId);

        var members = await _memberRepo.GetMembersByFamilyIdAsync(familyId);
        return members.Select(ToMemberDto.ToDto).ToList();
    }

    public async Task<MemberDto> UpdateMemberAsync(Guid id, UpdateMemberDto dto)
    {
        await _updateValidator.EnsureValidOrThrowAsync(dto);

        var member = await _memberRepo.GetMemberByIdAsync(id)
            ?? throw new NotFoundException(nameof(Member), nameof(Member.Id), id);

        if (dto.FullNameEn is not null)
            member.FullNameEn = dto.FullNameEn;

        if (dto.FullNameNe is not null)
            member.FullNameNe = dto.FullNameNe;

        if (dto.DateOfBirth.HasValue)
            member.DateOfBirth = dto.DateOfBirth.Value;

        if (dto.MobileNumber is not null)
            member.MobileNumber = dto.MobileNumber;

        if (dto.Email is not null)
            member.Email = dto.Email;

        if (dto.GenderId.HasValue)
            member.GenderId = dto.GenderId;

        if (dto.MartialStatusId.HasValue)
            member.MaritalStatusId = dto.MartialStatusId;

        if (dto.RelationshipToHeadId.HasValue)
            member.RelationshipToHeadId = dto.RelationshipToHeadId;

        if (dto.IdTypeId.HasValue)
            member.IdTypeId = dto.IdTypeId;

        if (dto.EducationLevelId.HasValue)
            member.EducationLevelId = dto.EducationLevelId;

        if (dto.OccupationId.HasValue)
            member.OccupationId = dto.OccupationId;

        var updated = await _memberRepo.UpdateMemberAsync(member);
        return ToMemberDto.ToDto(updated);
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        _ = await _memberRepo.GetMemberByIdAsync(id)
            ?? throw new NotFoundException(nameof(Member), nameof(Member.Id), id);

        await _memberRepo.DeleteMemberAsync(id);
    }
}
