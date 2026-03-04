using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MIS.API.Data;
using MIS.API.Dtos;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;
using Npgsql.Internal;

namespace MIS.API.Repositories;

public class ReligionRepo(AppDbContext context) : IReligionRepo
{
    private readonly AppDbContext _context = context;

    
    // GET BY ID
    public Task<Religion> GetReligionByIdAsync(Guid id)
    {
        var religion =  _context.Religions.FirstOrDefault(x=>x.Id == id);

        if(religion !=null)
        {
            return Task.FromResult(religion);
        }

        throw new KeyNotFoundException($"Religion with id{id} not found.");

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

        throw new KeyNotFoundException($"Religion not found");
    }
    
    //ADD
    public async Task<Religion> AddReligionAsync(string nameEn, string nameNe)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new KeyNotFoundException($"Religion name (EN) is required");

        if (string.IsNullOrWhiteSpace(nameNe))
            throw new Exception($"Religion name (NE) is required");

        var newReligion = new Religion
        {
            Id = Guid.NewGuid(),
            NameEn = nameEn,
            NameNe = nameNe
        };

        await _context.Religions.AddAsync(newReligion);
        await _context.SaveChangesAsync();

        return newReligion; 
    }

   

    // UPDATE
    public  Task<Religion> UpdateReligionAsync(Guid id, string nameEn, string nameNe)
    {
        var existingReligion =  _context.Religions.FirstOrDefault(x=>x.Id ==id);

        if (existingReligion != null)
        {
            if(!string.IsNullOrEmpty(nameEn))
            existingReligion.NameEn = nameEn;

              if(!string.IsNullOrEmpty(nameNe))
            existingReligion.NameNe = nameNe;

            _context.SaveChanges();
            return Task.FromResult(existingReligion);
        }

        throw new KeyNotFoundException($"Religion with id{id} not found.");
    }

    // DELETE
    public async Task DeleteReligionAsync(Guid id)
    {
        var religion = await _context.Religions.FindAsync(id);

        if (religion == null)
            throw new KeyNotFoundException($"Religion not found");

        _context.Religions.Remove(religion);
        await _context.SaveChangesAsync();
    }
}