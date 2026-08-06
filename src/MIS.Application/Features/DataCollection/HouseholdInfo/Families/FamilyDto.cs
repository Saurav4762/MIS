using System;
using System.Collections.Generic;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public class FamilyDto
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public Guid ResidenceHouseId { get; set; }
    public Guid? ResidentTypeId { get; set; }

    // Basic list of members (Create together)
    public IList<FamilyMemberDto> Members { get; set; } = new List<FamilyMemberDto>();

    // Can extend with sub-forms here
}