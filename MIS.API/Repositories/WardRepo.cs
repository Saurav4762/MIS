using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class WardRepo : IWardRepo
{
    private readonly AppDbContext _context;

    public WardRepo(AppDbContext context)
    {
        _context = context;
    }
    
    //GET
    public async Task<List<Ward>> GetAllWardsAsync()
    {
        return await _context.Wards.ToListAsync();
    }
    
    //GET BY ID
    public async Task<Ward?> GetWardByIdAsync(Guid id)
    {
        return await _context.Wards
            .FirstOrDefaultAsync(w => w.Id == id)
            ?? throw new NotFoundException(entity: nameof(Ward),key: nameof(Ward.Id), value: id);
    }

    public async Task<Ward> CreateWardAsync(string name, string code, Guid MunicipalityId)
    {

        var ward = new Ward
        {
            Id = Guid.NewGuid(),
            MunicipalityId = MunicipalityId,
            Name = name,
            Code = code
        };
        
        await _context.Wards.AddAsync(ward);
        await _context.SaveChangesAsync();
        
        return ward;

    }

    
    //UPDATE
    public async Task<Ward?> UpdateAsync(Guid id, string name, string code)
    {
        var existingWard = _context.Wards.FirstOrDefault(w => w.Id == id);
        
        if (existingWard == null)
            throw new NotFoundException(entity: nameof(Ward), key: nameof(Ward.Id), value: id);

        if (!string.IsNullOrEmpty(name))
            existingWard.Name = name;
        
        if (!string.IsNullOrEmpty(code))
            existingWard.Code = code;

        _context.SaveChanges();
        return await Task.FromResult(existingWard);


    }

    public async Task DeleteWardById(Guid id)
    {
        var ward = await _context.Wards.FindAsync(id);

        if (ward == null)
            throw new KeyNotFoundException($"Ward not found");
        
        _context.Wards.Remove(ward);
        await _context.SaveChangesAsync();

    }
}