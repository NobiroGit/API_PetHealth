using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Queries.AppUsers;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IAppUserRepository: 
    IQueryHandlerAsync<GetAllAppUserAsync, Result<IEnumerable<AppUserDto>>>,
    IQueryHandlerAsync<GetAppUserByIdAsync, Result<AppUserDto?>>,
    ICommandHandlerAsync<InsertAppUserCommandAsync, Result<int>>,
    ICommandHandlerAsync<DeleteAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdateAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdateEmailAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdatePasswordAppUserCommandAsync, Result>
{
    
}