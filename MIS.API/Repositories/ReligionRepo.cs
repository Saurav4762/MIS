using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MIS.API.Data;
using MIS.API.Dtos;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;
using Npgsql.Internal;

namespace MIS.API.Repositories;

public class ReligionRepo(AppDbContext context) : IReligionRepo
{
    private readonly AppDbContext _context = context;


    // GET BY ID
    public Task<Religion> GetReligionByIdAsync(Guid id)
    {
        var religion = _context.Religions.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException(entity: nameof(Religion), key: nameof(Religion.Id), value: id);

        return Task.FromResult(religion);
    }

    // GET ALL
    public async Task<List<Religion>> GetReligionsAsync()
    {
        return await _context.Religions.ToListAsync();
    }

    //ADD
    public async Task<Religion> AddReligionAsync(string nameEn, string nameNe)
    {
        var errors = new Dictionary<string, string[]>();
        if (string.IsNullOrWhiteSpace(nameEn))
            errors.Add(nameof(Religion.NameEn), ["Field is required"]);

        if (string.IsNullOrWhiteSpace(nameNe))
            errors.Add(nameof(Religion.NameNe), ["Field is required"]);

        if (errors.Count > 0) throw new ValidationException(errors: errors);

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
    public Task<Religion> UpdateReligionAsync(Guid id, string nameEn, string nameNe)
    {
        var ExistingReligion = _context.Religions.FirstOrDefault(x => x.Id == id);

        if (ExistingReligion == null)
            throw new NotFoundException(entity: nameof(Religion), key: nameof(Religion.Id), value: id);

        if (!string.IsNullOrEmpty(nameEn))
            ExistingReligion.NameEn = nameEn;

        if (!string.IsNullOrEmpty(nameNe))
            ExistingReligion.NameNe = nameNe;

        _context.SaveChanges();
        return Task.FromResult(ExistingReligion);
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