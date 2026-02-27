using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Dtos;
using MIS.API.Models;

namespace MIS.API.Repositories;

public class ReligionRepo : IReligionRepo
{
    private readonly AppDbContext _context;

    public ReligionRepo(AppDbContext context)
    {
        _context = context;
    }

    // GET BY ID
    public async Task<Religion> GetReligionByIdAsync(Guid id)
    {
        var religion = await _context.Religions
            .Where(r => r.Id == id)
            .Select(r => new Religion
            {
                Id = r.Id,
                NameEn = r.NameEn,
                NameNe = r.NameNe
            })
            .FirstOrDefaultAsync();

        if (religion == null)
            throw new Exception("Religion not found");

        return religion;
    }

    // GET ALL
    public async Task<List<Religion>> GetReligionsAsync()
    {
        return await _context.Religions
            .Select(r => new Religion
            {
                Id = r.Id,
                NameEn = r.NameEn,
                NameNe = r.NameNe
            })
            .ToListAsync();
    }
    
    //ADD
    public async Task<Religion> AddReligionAsync(ReligionRequestDto dtos)
    {
        if (string.IsNullOrWhiteSpace(dtos.NameEn))
            throw new Exception("Religion name (EN) is required");

        if (string.IsNullOrWhiteSpace(dtos.NameNe))
            throw new Exception("Religion name (NE) is required");

        var newReligion = new Religion
        {
            Id = Guid.NewGuid(),
            NameEn = dtos.NameEn,
            NameNe = dtos.NameNe
        };

        await _context.Religions.AddAsync(newReligion);
        await _context.SaveChangesAsync();

        return newReligion; 
    }

   

    // UPDATE
    public async Task<Religion> UpdateReligionAsync(Guid id, ReligionRequestDto religion)
    {
        var existingReligion = await _context.Religions.FindAsync(id);

        if (existingReligion == null)
            throw new Exception("Religion not found");

        existingReligion.NameEn = religion.NameEn;
        existingReligion.NameNe = religion.NameNe;

        await _context.SaveChangesAsync();
        return existingReligion;
    }

    // DELETE
    public async Task DeleteReligionAsync(Guid id)
    {
        var religion = await _context.Religions.FindAsync(id);

        if (religion == null)
            throw new Exception("Religion not found");

        _context.Religions.Remove(religion);
        await _context.SaveChangesAsync();
    }
}