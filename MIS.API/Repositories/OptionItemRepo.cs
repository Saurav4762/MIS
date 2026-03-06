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

  public async Task<OptionItemResponseDTO> GetOptionItemByIdAsync(Guid id)
  {
    var r = await _context.OptionItems.FirstOrDefaultAsync(x => x.Id == id)
    ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);

    return new OptionItemResponseDTO
    {
      Code = r.Code,
      Extra = r.Extra,
      Id = r.Id,
      LabelEn = r.LabelEn,
      LabelNe = r.LabelNe
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

  public async Task<OptionItemResponseDTO> UpdateOptionItemAsync(Guid id, UpdateOptionItemRequestDTO requestDTO)
  {
    var optionItem = await _context.OptionItems.FirstOrDefaultAsync(x => x.Id == id)
    ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);

    if (!string.IsNullOrEmpty(requestDTO.Code))
      optionItem.Code = requestDTO.Code;
    if (!string.IsNullOrEmpty(requestDTO.LabelEn))
      optionItem.LabelEn = requestDTO.LabelEn;
    if (!string.IsNullOrEmpty(requestDTO.LabelNe))
      optionItem.LabelNe = requestDTO.LabelNe;
    if (requestDTO.Extra != null)
      optionItem.Extra = requestDTO.Extra;

    if (requestDTO.SortOrder != null && requestDTO.SortOrder != 0)
      optionItem.SortOrder = requestDTO.SortOrder;


    _context.OptionItems.Update(optionItem);
    await _context.SaveChangesAsync();

    return new OptionItemResponseDTO
    {
      Id = optionItem.Id,
      Code = optionItem.Code,
      Extra = optionItem.Extra,
      LabelEn = optionItem.LabelEn,
      LabelNe = optionItem.LabelNe,
      SortOrder = optionItem.SortOrder
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