namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public interface ILivestockService
{
    Task<LivestockDto> GetLivestockByFamilyIdAsync(Guid familyId);
    Task<LivestockDto> CreateLivestockAsync(CreateLivestockDto createLivestockDto);
    Task<LivestockDto> UpdateLivestockAsync(Guid familyId, UpdateLivestockDto updateLivestockDto);
}
