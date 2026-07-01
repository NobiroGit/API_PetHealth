using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Queries.Pets;
using PetHealth.Application.Repositories;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.Pets;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class PetService : IPetRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public PetService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<PetDto>>> Execute(GetAllPetsQueryAsync query)
    {
        try
        {
            List<Pet> pets = (await _dbConnection.QueryAsync<Pet>("Usp_Pet_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!pets.Any()) return Result<IEnumerable<PetDto>>.Failure(Error.NotFound);

            return Result<IEnumerable<PetDto>>.Success(pets.Select(p => p.MapToPetDto()).ToList());
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<PetDto>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result<PetDto?>> Execute(GetPetByIdQueryAsync query)
    {
        try
        {
            Pet? pet = await _dbConnection.QueryFirstOrDefaultAsync<Pet>("Usp_Pet_GetById",
                commandType: CommandType.StoredProcedure,
                param: _currentUserRepository.WithUser(query));

            if (pet == null) return Result<PetDto?>.Failure(Error.NotFound);

            return Result<PetDto?>.Success(pet.MapToPetDto());
        }
        catch (SqlException e)
        {
            return Result<PetDto?>.Failure(new Error(e.ToError()));
        }
    }

    #endregion

    #region POST

    public async Task<Result<int>> Execute(InsertPetCommandAsync command)
    {
        try
        {
            var parameters = _currentUserRepository.WithUser(command);
            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Insert",
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

    #endregion

    #region PUT

    public async Task<Result> Execute(UpdatePetCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Update",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    #endregion

    #region PATCH

    public async Task<Result> Execute(UpdatePetPseudoCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Update_Pseudo",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeletePetCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    #endregion
}