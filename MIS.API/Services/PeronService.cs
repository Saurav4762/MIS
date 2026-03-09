using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Interfaces.IServices;
using MIS.API.Models;

namespace MIS.API.Services;

public class PeronService : IPersonService
{
    private readonly IPersonRepo _personRepo;

    public PeronService(IPersonRepo personRepo)
    {
        _personRepo = personRepo;
    }

    public async Task<PersonDTO.PersonResponseDto> CreateAsync(PersonDTO.CreatePersonDto dto)
    {

        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            FatherName = dto.FatherName,
            Gender = dto.Gender,
            BloodGroup = dto.BloodGroup,
            Email = dto.Email,
            BirthPlace = dto.BirthPlace,
            DateOfBirth = dto.DateOfBirth
        };

        await _personRepo.CreatePersonAsync(person);

        return new PersonDTO.PersonResponseDto
        {
            Id = person.Id,
            FullName = $"{person.FirstName} {person.MiddleName} {person.LastName}",
            Gender = person.Gender,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth
        };
    }

    public async Task<IEnumerable<PersonDTO.PersonResponseDto>> GetAllAsync()
    {
        var persons = await _personRepo.GetAllPersonsAsync();

        return persons.Select(p => new PersonDTO.PersonResponseDto
        {
            Id = p.Id,
            FullName = $"{p.FirstName} {p.MiddleName} {p.LastName}",
            Gender = p.Gender,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth
        });

    }

    public async Task<PersonDTO.PersonResponseDto?> GetByIdAsync(Guid id)
    {
        var person = await _personRepo.GetPersonByIdAsync(id);

        if (person == null)
            throw new NotFoundException(entity: nameof(person), key: nameof(person.Id), value: id);

        return new PersonDTO.PersonResponseDto
        {
            Id = person.Id,
            FullName = $"{person.FirstName} {person.MiddleName} {person.LastName}",
            Gender = person.Gender,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth
        };
    }

    public async Task UpdateAsync(Guid id, PersonDTO.CreatePersonDto dto)
    {
        var person = await _personRepo.GetPersonByIdAsync(id);
        
        if(person ==null )
            throw new NotFoundException(entity: nameof(person), key: nameof(person.Id), value: id);
        
        person.FirstName = dto.FirstName;
        person.MiddleName = dto.MiddleName;
        person.LastName = dto.LastName;
        person.FatherName = dto.FatherName;
        person.Gender = dto.Gender;
        person.BloodGroup = dto.BloodGroup;
        person.Email = dto.Email;
        person.BirthPlace = dto.BirthPlace;
        person.DateOfBirth = dto.DateOfBirth;

        await _personRepo.UpdatePersonAsync(person);
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await _personRepo.GetPersonByIdAsync(id); 
        
        if (person == null)
            throw new NotFoundException(entity: nameof(person), key: nameof(person.Id), value: id);
        
        await _personRepo.DeleteAsync(person);
    }
}