using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUserRoles;

public class RemoveUserRoleCommandAsync: ICommandDefinitionAsync<Result>
{
    public int AppUserId { get; init; }
    public int RoleId { get; init; }

    public RemoveUserRoleCommandAsync(AppUserRoleDto dto)
    {
        AppUserId = dto.AppUserId;
        RoleId = dto.RoleId;
    }
}