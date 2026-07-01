using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class RegisterAppUserCommandAsync: ICommandDefinitionAsync<Result>
{
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }

    public RegisterAppUserCommandAsync(RegisterAppUserDto dto)
    {
        LastName = dto.LastName;
        FirstName = dto.FirstName;
        Email = dto.Email;
        Password = dto.Password;
        Phone = dto.Phone;
        Address = dto.Address;
    }
}