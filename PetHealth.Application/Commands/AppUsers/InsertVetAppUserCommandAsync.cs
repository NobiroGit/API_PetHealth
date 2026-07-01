using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class InsertVetAppUserCommandAsync: ICommandDefinitionAsync<Result<int>>
{
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }
    public string Specialty { get; init; }

    public InsertVetAppUserCommandAsync(InsertVetAppUserDto dto)
    {
        LastName = dto.LastName;
        FirstName = dto.FirstName;
        Email = dto.Email;
        Password = dto.Password;
        Phone = dto.Phone;
        Address = dto.Address;
        Specialty = dto.Specialty;
    }
}