using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Queries.AppUsers;

namespace PetHealth.Application.Repositories;

public interface IAppUserRepository: 
    IQueryHandlerAsync<GetAllAppUserAsync, Result<IEnumerable<AppUserDto>>>,
    IQueryHandlerAsync<GetAppUserByIdAsync, Result<AppUserDto?>>,
    IQueryHandlerAsync<GetAppUserByJwtAsync, Result<AppUserDto>>,
    ICommandHandlerAsync<InsertVetAppUserCommandAsync, Result<int>>,
    ICommandHandlerAsync<DeleteAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdateAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdateEmailAppUserCommandAsync, Result>,
    ICommandHandlerAsync<UpdatePasswordAppUserCommandAsync, Result>,
    ICommandHandlerAsync<LoginAppUserCommandAsync, Result<JwtInfoAppUserDto>>,
    ICommandHandlerAsync<RegisterAppUserCommandAsync, Result>
{
    
}