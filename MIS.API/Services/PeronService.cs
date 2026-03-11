using Microsoft.EntityFrameworkCore;
using MIS.API.DTOs;
using MIS.API.Enum;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Interfaces.IServices;
using MIS.API.Models;
using static MIS.API.DTOs.PersonDTO;

namespace MIS.API.Services;

public class PeronService : IPersonService
{
    private readonly IPersonRepo _personRepo;

    public PeronService(IPersonRepo personRepo)
    {
        _personRepo = personRepo;
    }

    private void ValidateCreatePerson(CreatePersonDto dto)
    {
        var errors = new Dictionary<string, string[]>();
        
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors["FirstName"] = ["First name is required"];
        
        if(dto.FirstName.Any(char.IsDigit))
            errors["FirstName"] = ["First name contains digits"];
        
        if(string.IsNullOrWhiteSpace(dto.MiddleName))
            errors["MiddleName"] = ["Middle name is required"];
        
        if(dto.MiddleName.Any(char.IsDigit))
            errors["MiddleName"] = ["Middle name contains digits"];
        
        if(string.IsNullOrWhiteSpace(dto.LastName))
            errors["LastName"] = ["Last name is required"];
        
        if(dto.LastName.Any(char.IsDigit))
            errors["LastName"] = ["Last name contains digits"];
        
        if (string.IsNullOrWhiteSpace(dto.Email))
            errors["Email"] = ["Email is required"];
        else
        {
            try
            {
                var emailCheck = new System.Net.Mail.MailAddress(dto.Email);

            }
            catch 
            {
               errors["Email"] = ["Email is required"];
            }
        }
        
        if(string.IsNullOrWhiteSpace(dto.BirthPlace))
            errors["BirthPlace"] = ["Birth place is required"];
        
        if(string.IsNullOrWhiteSpace(dto.BloodGroup))
            errors["BloodGroup"] = ["Blood group is required"];
        
        if(string.IsNullOrWhiteSpace(dto.FatherName))
            errors["FatherName"] = ["Father name is required"];
        
        if(dto.DateOfBirth> DateTime.UtcNow)
            errors["DateOfBirth"] = ["Date of birth is required"];
        
        if(!System.Enum.IsDefined(typeof(Gender), dto.Gender))
        errors.Add("Gender", new[] {"Gender must be Male. Female or Others" });

        if (errors.Any())
            throw new ValidationException(errors);
    }

    public async Task<PersonResponseDto> CreateAsync(CreatePersonDto dto)
    {
        ValidateCreatePerson(dto);

        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            FatherName = dto.FatherName,
            Gender =  dto.Gender,
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
        ValidateCreatePerson(dto);
        
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
        var errors = new Dictionary<string, string[]>();
        var person = await _personRepo.GetPersonByIdAsync(id); 
        
        if (person == null)
            throw new NotFoundException(entity: nameof(person), key: nameof(person.Id), value: id);

        try
        {
            await _personRepo.DeleteAsync(person);

        }
        catch (DbUpdateException dbEx)
        {
            errors["Deleted"] = ["Person cannot be deleted it is reference in another record"];

        }
        catch (Exception ex)
        {
            //Re-throw unexpected errors to global exception handler
            throw;
        }
    }
}