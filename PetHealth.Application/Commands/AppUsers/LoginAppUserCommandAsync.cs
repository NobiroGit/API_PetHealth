using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class LoginAppUserCommandAsync : ICommandDefinitionAsync<Result<JwtInfoAppUserDto>>
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginAppUserCommandAsync(LoginAppUserDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
    }
}