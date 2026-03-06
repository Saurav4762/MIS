using Microsoft.EntityFrameworkCore.ChangeTracking;
using MIS.API.DTOs;
using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;


public interface IOptionItemRepo
{
  public Task<OptionItemsResponseDTO> CreateOptionItemAsync(OptionItemsRequestDTO requestDTO);
  public Task<OptionItemsResponseDTO> GetOptionItemsByOptionListIdAsync(Guid optionListId);
  public Task<OptionItemResponseDTO> GetOptionItemByIdAsync(Guid id);

  public Task<OptionItemResponseDTO> UpdateOptionItemAsync(Guid id, UpdateOptionItemRequestDTO requestDTO);
  public Task DeleteOptionItemByIdAsync(Guid id);


}