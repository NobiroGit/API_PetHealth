using System.Data;
using Dapper;
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


    #region LOGIN / REGISTER

    public async Task<Result<JwtInfoAppUserDto>> Execute(LoginAppUserCommandAsync command)
    {
        var jwtInfo = await _dbConnection.QueryFirstOrDefaultAsync<JwtInfoAppUserDto>("Usp_AppUser_Login",
            commandType: CommandType.StoredProcedure, param: command);
        if (jwtInfo == null)
            return Result<JwtInfoAppUserDto>.Failure(Error.NotFound);

        return Result<JwtInfoAppUserDto>.Success(jwtInfo);
    }

    public async Task<Result> Execute(RegisterAppUserCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Register",
            commandType: CommandType.StoredProcedure, param: command);
        if (rows == 1) return Result.Success();
        return Result.Failure(Error.NotFound);
    }

    #endregion

    #region GET

//ADMIN
    public async Task<Result<IEnumerable<AppUserDto>>> Execute(GetAllAppUserAsync query)
    {
        List<AppUser> petOwners =
            (await _dbConnection.QueryAsync<AppUser>("Usp_AppUser_GetAll",
                param: _currentUserRepository.WithUser(query)))
            .ToList();
        if (!petOwners.Any()) return Result<IEnumerable<AppUserDto>>.Failure(Error.NotFound);

        return Result<IEnumerable<AppUserDto>>.Success(petOwners.Select(i => i.toAppUserDto()));
    }

//ADMIN
    public async Task<Result<AppUserDto?>> Execute(GetAppUserByIdAsync query)
    {
        AppUser? petOwner =
            await _dbConnection.QueryFirstOrDefaultAsync<AppUser>("Usp_AppUser_GetById",
                param: _currentUserRepository.WithUser(query));
        if (petOwner == null) return Result<AppUserDto?>.Failure(Error.NotFound);

        return Result<AppUserDto?>.Success(petOwner.toAppUserDto());
    }

    #endregion

    #region POST

    public async Task<Result<int>> Execute(InsertVetAppUserCommandAsync command)
    {
        var parameters = _currentUserRepository.WithUser(command);
        parameters.Add("NewId", DbType.Int32, direction: ParameterDirection.Output);

        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_InsertVet",
            commandType: CommandType.StoredProcedure, param: parameters);

        if (rows > 1)
        {
            int newId = parameters.Get<int>("@NewId");
            return Result<int>.Success(newId);
        }

        return Result<int>.Failure(Error.NotFound);
    }

    #endregion

    #region PUT

    public async Task<Result> Execute(UpdateAppUserCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        if (rows == 1) return Result.Success();
        return Result.Failure(Error.NotFound);
    }

    #endregion

    #region PATCH

    public async Task<Result> Execute(UpdateEmailAppUserCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update_Email",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        if (rows == 1) return Result.Success();
        return Result.Failure(Error.NotFound);
    }

    public async Task<Result> Execute(UpdatePasswordAppUserCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Update_Password",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        if (rows == 1) return Result.Success();
        return Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteAppUserCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_AppUser_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        if (rows == 1) return Result.Success();

        return Result.Failure(Error.NotFound);
    }

    #endregion
}