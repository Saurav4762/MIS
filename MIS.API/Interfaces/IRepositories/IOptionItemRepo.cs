using Microsoft.EntityFrameworkCore.ChangeTracking;
using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;


public interface IOptionItemRepo
{
  public Task<OptionItem> CreateOptionItemAsync();
  public Task<List<OptionItem>> GetOptionItemsByOptionListId();

}