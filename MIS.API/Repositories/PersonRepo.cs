using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;

namespace MIS.API.Repositories;

public class PersonRepo : IPersonRepo
{
    
    private readonly AppDbContext _context;

    public PersonRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        return await _context.Persons.FirstOrDefaultAsync(x =>x.Id == id);
    }

    public async Task UpdatePersonAsync(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
}