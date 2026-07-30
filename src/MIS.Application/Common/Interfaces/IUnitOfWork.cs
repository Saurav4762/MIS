using MIS.Application.Features.Authentication;
using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Economies;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;
using MIS.Application.Features.DataCollection.HouseholdInfo.Healths;
using MIS.Application.Features.DataCollection.HouseInfo;
using MIS.Application.Features.Geography.Areas;
using MIS.Application.Features.Geography.Districts;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Application.Features.Geography.Provinces;
using MIS.Application.Features.Geography.Toles;
using MIS.Application.Features.Geography.Wards;
using MIS.Application.Features.Options.OptionItems;
using MIS.Application.Features.Options.OptionLists;
using MIS.Application.Features.Submissions;
using MIS.Application.Features.Users;

namespace MIS.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IAuthenticationRepository AuthenticationRepository { get; }
    IUserRepository UserRepository { get; }
    IOptionListRepo OptionListRepository { get; }
    IOptionItemRepo OptionItemRepository { get; }
    IMunicipalityRepo MunicipalityRepository { get; }
    IDistrictRepo DistrictRepository { get; }
    IProvinceRepo ProvinceRepository { get; }
    IWardRepo WardRepository { get; }
    IAreaRepo AreaRepository { get; }
    IToleRepo ToleRepository { get; }
    ISubmissionsRepo SubmissionsRepository { get; }
    IHouseInfoRepo HouseInfoRepository { get; }
    IDecisionRepo DecisionRepository { get; }
    IEconomyRepo EconomyRepository { get; }
    IFacilityRepo FacilityRepository { get; }
    IHealthRepo HealthRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
