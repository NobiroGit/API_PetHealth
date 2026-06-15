using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Queries.AppUsers;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class AppUserService : IAppUserRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public AppUserService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

//ADMIN
    public async Task<Result<IEnumerable<AppUserDto>>> Execute(GetAllAppUserAsync query)
    {
        try
        {
            List<AppUser> petOwners =
                (await _dbConnection.QueryAsync<AppUser>("Usp_AppUser_GetAll", param: _currentUserRepository.WithUser(query)))
                .ToList();
            if (!petOwners.Any()) return Result<IEnumerable<AppUserDto>>.Failure(Error.NotFound);

            return Result<IEnumerable<AppUserDto>>.Success(petOwners.Select(i => i.toAppUserDto()));
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<AppUserDto>>.Failure(new Error(e.ToError()));
        }
    }

//ADMIN
    public async Task<Result<AppUserDto?>> Execute(GetAppUserByIdAsync query)
    {
        try
        {
            AppUser? petOwner =
                await _dbConnection.QueryFirstOrDefaultAsync<AppUser>("Usp_AppUser_GetById",
                    param: _currentUserRepository.WithUser(query));
            if (petOwner == null) return Result<AppUserDto?>.Failure(Error.NotFound);

            return Result<AppUserDto?>.Success(petOwner.toAppUserDto());
        }
        catch (SqlException e)
        {
            return Result<AppUserDto?>.Failure(new Error(e.ToError()));
        }
    }

//ADMIN
    public async Task<Result<int>> Execute(InsertAppUserCommandAsync command)
    {
        try
        {
            var parameters = _currentUserRepository.WithUser(command);
            parameters.Add("NewId", DbType.Int32, direction: ParameterDirection.Output);

            int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Insert",
                commandType: CommandType.StoredProcedure, param: parameters);

            if (rows == 1)
            {
                int newId = parameters.Get<int>("@NewId");
                return Result<int>.Success(newId);
            }

            return Result<int>.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result<int>.Failure(new Error(e.ToError()));
        }
    }

//ADMIN
    public async Task<Result> Execute(DeleteAppUserCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            if (rows == 1) return Result.Success();

            return Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

//ADMIN
    public async Task<Result> Execute(UpdateAppUserCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            if (rows == 1) return Result.Success();
            return Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(UpdateEmailAppUserCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update_Email",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            if (rows == 1) return Result.Success();
            return Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(UpdatePasswordAppUserCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update_Password",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            if (rows == 1) return Result.Success();
            return Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }
}