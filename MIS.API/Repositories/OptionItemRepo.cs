using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;

namespace MIS.API.Repositories;


public class OptionItemRepo(AppDbContext context) : IOptionItemRepo
{
  public AppDbContext _context = context;

  public async Task<OptionItemsResponseDTO> CreateOptionItemAsync(OptionItemsRequestDTO requestDTO)
  {
    List<OptionItem> newOptionItems = [];
    var optionList = await _context.OptionLists.FirstOrDefaultAsync(x => x.Id == requestDTO.OptionListId)
    ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), requestDTO.OptionListId);

    foreach (var item in requestDTO.Items)
    {
      newOptionItems.Add(new OptionItem
      {
        Id = Guid.NewGuid(),
        OptionListId = requestDTO.OptionListId,
        Code = item.Code,
        LabelEn = item.LabelEn,
        LabelNe = item.LabelNe,
        Extra = item.Extra,
        SortOrder = item.SortOrder
      });
    }

    await _context.OptionItems.AddRangeAsync(newOptionItems);
    await _context.SaveChangesAsync();
    return new OptionItemsResponseDTO
    {
      OptionListId = requestDTO.OptionListId,
      Items = [.. newOptionItems.Select(x => new OptionItemResponseDTO
      {
        Id = x.Id,
        Code = x.Code,
        LabelEn = x.LabelEn,
        LabelNe = x.LabelNe,
        Extra = x.Extra,
        SortOrder = x.SortOrder
      })]
    };
  }


  public async Task<OptionItemsResponseDTO> GetOptionItemsByOptionListIdAsync(Guid optionListId)
  {
    var optionList = await _context.OptionLists.FirstOrDefaultAsync(x => x.Id == optionListId)
    ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), optionListId);

    var optionItems = await _context.OptionItems.Where(x => x.OptionListId == optionListId)
    .Select(x => new OptionItemResponseDTO
    {
      Id = x.Id,
      Code = x.Code,
      LabelEn = x.LabelEn,
      LabelNe = x.LabelNe,
      Extra = x.Extra,
      SortOrder = x.SortOrder
    })
    .ToListAsync();

    return new OptionItemsResponseDTO
    {
      OptionListId = optionListId,
      Items = optionItems
    };

  }

  public async Task DeleteOptionItemByIdAsync(Guid id)
  {
    var optionItem = await _context.OptionItems.FirstOrDefaultAsync(x => x.Id == id)
    ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);

    _context.OptionItems.Remove(optionItem);
    await _context.SaveChangesAsync();
  }
}