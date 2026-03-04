using MIS.API.Models;

namespace MIS.API.Repositories.Interfaces;

public interface IMunicipalityRepo
{
     Task<Municipality> CreateMunicipality(string nameEn, string nameNe, string code);
     Task<Municipality?> GetById(Guid id);
     Task<Municipality> UpdateMunicipality(Guid id, string nameEn, string nameNe, string code);
     Task<Municipality> DeleteMunicipality(Guid id);
}