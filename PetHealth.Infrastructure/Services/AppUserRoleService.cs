using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Commands.AppUserRoles;
using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.AppUserRoles;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class AppUserRoleService : IAppUserRoleRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public AppUserRoleService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    public async Task<Result<IEnumerable<AppUserRoleDto>>> Execute(GetAllAppUserRoleQueriesAsync query)
    {
        try
        {
            var appUserRoles = (await _dbConnection.QueryAsync<AppUserRole>("Usp_AppUserRole_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).Select(i => i.ToAppUserRoleDto()).ToList();
            if (!appUserRoles.Any()) return Result<IEnumerable<AppUserRoleDto>>.Failure(Error.NotFound);
            return Result<IEnumerable<AppUserRoleDto>>.Success(appUserRoles);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<AppUserRoleDto>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(AssignUserRoleCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUserRole_Assign",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(RemoveUserRoleCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUserRole_Remove",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }
}