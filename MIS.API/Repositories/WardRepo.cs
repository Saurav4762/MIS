using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Interfaces.IRepositories;

namespace MIS.API.Repositories;

public class WardRepo : IWardRepo
{
    private readonly AppDbContext _context;

    public WardRepo(AppDbContext context)
    {
        _context = context;
    }
    
    //GET WITH PAGINATION
    public async Task<PaginatedResponse<Ward>> GetAllWardsAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _context.Wards.CountAsync();
        
        var wards = await _context.Wards
            .Include(w => w.Municipality)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<Ward>
        {
            Data = wards,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    
    //GET BY ID
    public async Task<Ward?> GetWardByIdAsync(Guid id)
    {
        return await _context.Wards
            .Include(w => w.Municipality)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<Ward> CreateWardAsync(Ward ward)
    {
        await _context.Wards.AddAsync(ward);
        await _context.SaveChangesAsync();
        
        return ward;
    }

    public async Task UpdateAsync(Ward ward)
    {
        _context.Wards.Update(ward); 
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWardById(Ward ward)
    {
        _context.Wards.Remove(ward);
        await _context.SaveChangesAsync();
    }

    public Task<bool> MunicipalityExistsAsync(Guid id)
    {
      return _context.Municipalities
          .AnyAsync(m => m.Id == id);
    }

    public async Task<bool> WardExistsAsync(string name, Guid MunicipalityId)
    {
        return await _context.Wards
            .AnyAsync(w => w.Name.ToLower() == name.ToLower() 
                           && w.MunicipalityId == MunicipalityId);
    }

    public async Task<bool> WardNameExistsAsync(string name, Guid MunicipalityId, Guid WardId)
    {
        return await _context.Wards
            .AnyAsync(w => w.Name.ToLower() == name.ToLower()
                            && w.MunicipalityId == MunicipalityId
                            &&w.Id != WardId);
    }

    public async Task<Ward?> GetWardWithMunicipality(Guid id)
    {
        return await _context.Wards
            .Include(w => w.Municipality)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> WardExistsByIdAsync(Guid id)
    {
        return await _context.Wards.AnyAsync(w => w.Id == id);
    }
}