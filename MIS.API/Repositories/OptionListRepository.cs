namespace MIS.API.Repositories;

using MIS.API.Data;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

public class OptionListRepository(AppDbContext context) : IOptionList
{
    private readonly AppDbContext _context = context;

    public Task<OptionList> CreateOptionListAsync(string code, string nameNe, string nameEn, string description)
    {
        var newOptionList = new OptionList
        {
            Id = Guid.NewGuid(),
            LabelEn = nameEn,
            Code = code,
            LabelNe = nameNe,
            Description = description
        };
        _context.OptionLists.Add(newOptionList);
        _context.SaveChanges();
        return Task.FromResult(newOptionList);
    }
    public Task<OptionList> GetOptionListByIdAsync(Guid id)
    {
        var optionList = _context.OptionLists.FirstOrDefault(x => x.Id == id);
        if (optionList != null)
        {
            return Task.FromResult(optionList);
        }
        throw new NotFoundException(nameof(OptionList), nameof(OptionItem.Id), id);
    }



    public Task<OptionList> UpdateOptionListAsync(Guid id, string nameEn, string nameNe, string description)
    {
        var optionList = _context.OptionLists.FirstOrDefault(x => x.Id == id);
        // select * from OptionLists where Id = id
        if (optionList != null)
        {
            if (!string.IsNullOrEmpty(nameEn))
                optionList.LabelEn = nameEn;
            if (!string.IsNullOrEmpty(nameNe))
                optionList.LabelNe = nameNe;
            if (!string.IsNullOrEmpty(description))
                optionList.Description = description;
            _context.SaveChanges();
            return Task.FromResult(optionList);
        }
        throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), id);
    }
    public Task DeleteOptionListAsync(Guid id)
    {
        var optionList = _context.OptionLists.FirstOrDefault(x => x.Id == id);
        if (optionList != null)
        {
            _context.OptionLists.Remove(optionList);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        throw new NotFoundException(nameof(OptionList), nameof(OptionList.Id), id);
    }
}
