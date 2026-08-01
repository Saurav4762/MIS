using System;
using System.Collections.Generic;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Famiiles;

public class FamilyDTO
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public Guid ResidenceHouseId { get; set; }
    public Guid? ResidentTypeId { get; set; }

    // Basic list of members (Create together)
    public IList<MemberDto> Members { get; set; } = new List<MemberDto>();

    // Can extend with sub-forms here
}