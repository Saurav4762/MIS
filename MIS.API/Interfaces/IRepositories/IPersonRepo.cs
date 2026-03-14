using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IPersonRepo
{
    Task<Person> CreatePersonAsync (Person person);
    
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    
    Task<Person> GetPersonByIdAsync(Guid id);
    
    Task UpdatePersonAsync (Person person);
    
    Task DeleteAsync (Person person);
}