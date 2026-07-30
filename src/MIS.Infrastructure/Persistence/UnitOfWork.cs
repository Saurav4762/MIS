using MIS.Application.Common.Interfaces;
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
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context,
        IAuthenticationRepository authenticationRepository,
        IUserRepository userRepository,
        IOptionListRepo optionListRepository,
        IOptionItemRepo optionItemRepository,
        IMunicipalityRepo municipalityRepository,
        IDistrictRepo districtRepository,
        IProvinceRepo provinceRepository,
        IWardRepo wardRepository,
        IAreaRepo areaRepository,
        IToleRepo toleRepository,
        ISubmissionsRepo submissionsRepository,
        IHouseInfoRepo houseInfoRepository,
        IDecisionRepo decisionRepository,
        IEconomyRepo economyRepository,
        IFacilityRepo facilityRepository,
        IHealthRepo healthRepository)
    {
        _context = context;
        AuthenticationRepository = authenticationRepository;
        UserRepository = userRepository;
        OptionListRepository = optionListRepository;
        OptionItemRepository = optionItemRepository;
        MunicipalityRepository = municipalityRepository;
        DistrictRepository = districtRepository;
        ProvinceRepository = provinceRepository;
        WardRepository = wardRepository;
        AreaRepository = areaRepository;
        ToleRepository = toleRepository;
        SubmissionsRepository = submissionsRepository;
        HouseInfoRepository = houseInfoRepository;
        DecisionRepository = decisionRepository;
        EconomyRepository = economyRepository;
        FacilityRepository = facilityRepository;
        HealthRepository = healthRepository;
    }

    public IAuthenticationRepository AuthenticationRepository { get; }
    public IUserRepository UserRepository { get; }
    public IOptionListRepo OptionListRepository { get; }
    public IOptionItemRepo OptionItemRepository { get; }
    public IMunicipalityRepo MunicipalityRepository { get; }
    public IDistrictRepo DistrictRepository { get; }
    public IProvinceRepo ProvinceRepository { get; }
    public IWardRepo WardRepository { get; }
    public IAreaRepo AreaRepository { get; }
    public IToleRepo ToleRepository { get; }
    public ISubmissionsRepo SubmissionsRepository { get; }
    public IHouseInfoRepo HouseInfoRepository { get; }
    public IDecisionRepo DecisionRepository { get; }
    public IEconomyRepo EconomyRepository { get; }
    public IFacilityRepo FacilityRepository { get; }
    public IHealthRepo HealthRepository { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
