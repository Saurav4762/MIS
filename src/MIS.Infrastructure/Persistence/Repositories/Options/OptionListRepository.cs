
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Options.OptionLists;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Options;

public class OptionListRepository : IOptionListRepo
{
  private readonly ApplicationDbContext _context;
  public OptionListRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<OptionList> CreateOptionListAsync(OptionList optionList)
  {
    await _context.OptionLists.AddAsync(optionList);
    await _context.SaveChangesAsync();
    return optionList;
  }

  public async Task<int> ExecuteDeleteAsync(Guid id)
  {
    return await _context.OptionLists
      .Where(x => x.Id == id)
      .ExecuteDeleteAsync();
  }

  public async Task<OptionList> Update(OptionList optionList)
  {
    _context.OptionLists.Update(optionList);
    await _context.SaveChangesAsync();
    return optionList;
  }

  public async Task<List<OptionList>> GetAllOptionListsAsync()
  {
    return await _context.OptionLists.ToListAsync();
  }

  public async Task<OptionList> GetOptionListByIdAsync(Guid id)
  {
    return await _context.OptionLists.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), id);
  }
}
