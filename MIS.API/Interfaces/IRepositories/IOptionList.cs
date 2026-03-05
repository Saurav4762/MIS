using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;


public interface IOptionList
{
  Task<OptionList> CreateOptionListAsync(string code, string nameEn, string nameNe, string description);
  Task<OptionList> GetOptionListByIdAsync(Guid id);
  Task<OptionList> UpdateOptionListAsync(Guid id, string nameEn, string nameNe, string description);
  Task DeleteOptionListAsync(Guid id);

}