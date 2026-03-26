
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Options;


public class OptionItemRepository : IOptionItemRepo
{
  private readonly ApplicationDbContext _context;

  public OptionItemRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<OptionItem> CreateOptionItemAsync(OptionItem optionItem)
  {
    await _context.OptionItems.AddAsync(optionItem);
    await _context.SaveChangesAsync();
    return optionItem;
  }

  public async Task<List<OptionItem>> GetOptionItemByOptionListIdAsync(Guid optionListId)
  {
    return await _context.OptionItems
      .Where(x => x.OptionListId == optionListId)
      .ToListAsync();
  }

  public async Task<OptionItem?> GetOptionItemByIdAsync(Guid id)
  {
    return await _context.OptionItems.FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<OptionItem> UpdateOptionItemAsync(OptionItem optionItem)
  {
    var existing = await _context.OptionItems.FirstOrDefaultAsync(x => x.Id == optionItem.Id)
      ?? throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), optionItem.Id);

    existing.LabelEn = optionItem.LabelEn;
    existing.LabelNe = optionItem.LabelNe;
    existing.Extra = optionItem.Extra;

    await _context.SaveChangesAsync();
    return existing;
  }

  public async Task ExecuteDeleteAsync(Guid id)
  {
    var rows = await _context.OptionItems
      .Where(x => x.Id == id)
      .ExecuteDeleteAsync();

    if (rows == 0)
      throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), id);
  }
}