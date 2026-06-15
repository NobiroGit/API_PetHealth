using PetHealth.Application.Commands.AppUserRoles;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.AppUserRoles;

namespace PetHealth.Application.Repositories;

public interface IAppUserRoleRepository: 
    IQueryHandlerAsync<GetAllAppUserRoleQueriesAsync, Result<IEnumerable<AppUserRoleDto>>>,
    ICommandHandlerAsync<AssignUserRoleCommandAsync, Result>,
    ICommandHandlerAsync<RemoveUserRoleCommandAsync, Result>
{
    
}