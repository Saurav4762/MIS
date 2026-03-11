using MIS.API.DTOs;


using static MIS.API.DTOs.PersonDTO;

namespace MIS.API.Interfaces.IServices;

public interface IPersonService
{
    Task<PersonDTO.PersonResponseDto> CreateAsync(CreatePersonDto dto);

    Task<IEnumerable<PersonResponseDto>> GetAllAsync();

    Task<PersonDTO.PersonResponseDto?> GetByIdAsync(Guid id);

    Task UpdateAsync(Guid id, CreatePersonDto dto);

    Task DeleteAsync(Guid id);
    
}