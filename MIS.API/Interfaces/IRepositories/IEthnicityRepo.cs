using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IEthnicityRepo
 {
     Task<Ethnicity> CreateEthnicity(string nameEn , string nameNe);
     Task<Ethnicity> GetById(Guid id);
     Task<Ethnicity> UpdateEthnicity(Guid id,string nameEn, string nameNe);
     Task<Ethnicity> DeleteEthnicity(Guid id);
 }