using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class UpdatePasswordAppUserCommandAsync: ICommandDefinitionAsync<Result>
{
    public string Password { get; init; }
    
    public UpdatePasswordAppUserCommandAsync(UpdatePasswordAppUserDto dto)
    {
        Password = dto.Password;
    }
}