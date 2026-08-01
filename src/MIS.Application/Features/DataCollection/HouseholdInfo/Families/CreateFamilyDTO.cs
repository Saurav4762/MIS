using System;
using System.Collections.Generic;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Famiiles;

public class CreateFamilyDTO
{
    public Guid SubmissionId { get; set; }
    public Guid ResidenceHouseId { get; set; }
    public Guid? ResidentTypeId { get; set; }
    
    //Basic list of members (Create together)
    public IList<CreateMemberDTO> Members { get; set; } = new List<CreateMemberDTO>();
    
    // Can extend with sub-forms here
}