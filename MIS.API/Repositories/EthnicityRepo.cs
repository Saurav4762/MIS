using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class EthnicityRepo : IEthnicityRepo
{
    private readonly AppDbContext _context;

    public EthnicityRepo(AppDbContext context)
    {
        _context = context;
    }

    public Task<Ethnicity> CreateEthnicity( string nameEn , string nameNe)
    {
        var newEthnicity = new Ethnicity
        {
            Id = Guid.NewGuid(),
            NameEn = nameEn,
            NameNe = nameNe,
        };
        _context.Ethnicities.Add(newEthnicity);
        _context.SaveChanges();
        return Task.FromResult(newEthnicity);
    }
    public async Task <Ethnicity> GetById(Guid id)
    {
        
        var ethnicity = await _context.Ethnicities.FindAsync(id);
        if (ethnicity == null)
        {
            throw new KeyNotFoundException($"Ethnicity with id {id} not found");
        }
        return ethnicity;
    }
    
    public async Task<Ethnicity> UpdateEthnicity(Guid id, string nameEn , string nameNe)
    {
        var ethnicity = await _context.Ethnicities.FirstOrDefaultAsync(x =>x.Id == id);
        if (ethnicity == null)
        {
            throw new KeyNotFoundException($"Ethnicity with id {id} not found");
        }
        ethnicity.NameEn = nameEn;
        ethnicity.NameNe = nameNe;
        _context.Ethnicities.Update(ethnicity);
        await _context.SaveChangesAsync();
        return ethnicity;
    }


    public async Task<Ethnicity> DeleteEthnicity(Guid id)
    {
        var ethnicity = await _context.Ethnicities.FirstOrDefaultAsync(x => x.Id == id);
        if (ethnicity == null)
        {
            throw new KeyNotFoundException($"Ethnicity with id {id} not found");
        }
        _context.Ethnicities.Remove(ethnicity);
        await _context.SaveChangesAsync();
        return ethnicity;
    }
}