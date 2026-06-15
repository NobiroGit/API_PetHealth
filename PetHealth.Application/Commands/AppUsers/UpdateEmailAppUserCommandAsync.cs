using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class UpdateEmailAppUserCommandAsync: ICommandDefinitionAsync<Result>
{
    public string Email { get; init; }

    public UpdateEmailAppUserCommandAsync(UpdateEmailAppUserDto dto)
    {
        Email = dto.Email;
    }
}