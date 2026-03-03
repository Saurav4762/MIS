using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class MunicipalityRepo : IMunicipalityRepo
{
    private readonly AppDbContext _context;

    public MunicipalityRepo(AppDbContext context)
    {
        _context = context;
    }

    public Task<Municipality> CreateMunicipality(string nameEn, string nameNe, string code)
    {
        var newMunicipality = new Municipality
        {
            Id = Guid.NewGuid(),
            NameEn = nameEn,
            NameNe = nameNe,
            Code = code
        };
        _context.Municipalities.Add(newMunicipality);
        _context.SaveChanges();
        return Task.FromResult(newMunicipality);
    }

    public async Task<Municipality> GetById(Guid id)
    {
        var municipality = await _context.Municipalities.FindAsync(id) ??
            throw new NotFoundException(
                entity: nameof(Municipality),
                key: nameof(Municipality.Id),
                value: id
            );

        return municipality;
    }

    public async Task<Municipality> UpdateMunicipality(Guid id, string nameNe, string nameEn, string code)
    {
        var municipality = await _context.Municipalities.FirstOrDefaultAsync(x => x.Id == id) ??
            throw new NotFoundException(
                entity: nameof(Municipality),
                key: nameof(Municipality.Id),
                value: id
            );

        municipality.NameEn = nameNe;
        municipality.NameNe = nameNe;
        municipality.Code = code;
        _context.Municipalities.Update(municipality);
        await _context.SaveChangesAsync();
        return municipality;
    }

    public async Task<Municipality> DeleteMunicipality(Guid id)
    {
        var municipality = await _context.Municipalities.FirstOrDefaultAsync(x => x.Id == id) ??
            throw new NotFoundException(
                entity: nameof(Municipality),
                key: nameof(Municipality.Id),
                value: id
            );
        _context.Municipalities.Remove(municipality);
        await _context.SaveChangesAsync();
        return municipality;
    }

}