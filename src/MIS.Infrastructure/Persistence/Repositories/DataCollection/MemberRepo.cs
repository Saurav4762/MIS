using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class MemberRepo : IMemberRepo
{
    private readonly ApplicationDbContext _context;

    public MemberRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Member> CreateMemberAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<Member?> GetMemberByIdAsync(Guid id)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Member>> GetMembersByFamilyIdAsync(Guid familyId)
    {
        return await _context.Members
            .Where(m => m.FamilyId == familyId)
            .ToListAsync();
    }

    public async Task<Member> UpdateMemberAsync(Member member)
    {
        var existing = await _context.Members.FirstOrDefaultAsync(m => m.Id == member.Id)
            ?? throw new NotFoundException(nameof(Member), nameof(Member.Id), member.Id);

        existing.FullNameEn = member.FullNameEn;
        existing.FullNameNe = member.FullNameNe;
        existing.DateOfBirth = member.DateOfBirth;
        existing.MobileNumber = member.MobileNumber;
        existing.Email = member.Email;
        existing.GenderId = member.GenderId;
        existing.MaritalStatusId = member.MaritalStatusId;
        existing.RelationshipToHeadId = member.RelationshipToHeadId;
        existing.IdTypeId = member.IdTypeId;
        existing.EducationLevelId = member.EducationLevelId;
        existing.OccupationId = member.OccupationId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        await _context.Members
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
    }
}
