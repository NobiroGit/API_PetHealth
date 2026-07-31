using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger _logger;
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public PetService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository,
        ILogger<PetService> logger)
    {
        _logger = logger;
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<PetDto>>> Execute(GetAllPetsQueryAsync query)
    {
        List<Pet> pets = (await _dbConnection.QueryAsync<Pet>("Usp_Pet_GetAll",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!pets.Any()) return Result<IEnumerable<PetDto>>.Failure(Error.NotFound);

        return Result<IEnumerable<PetDto>>.Success(pets.Select(p => p.MapToPetDto()).ToList());
    }

    public async Task<Result<IEnumerable<PetDto>>> Execute(GetMyPetsQueryAsync query)
    {
        List<Pet> pets = (await _dbConnection.QueryAsync<Pet>("usp_Pet_MyPets",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!pets.Any()) return Result<IEnumerable<PetDto>>.Failure(Error.NotFound);
        
        return Result<IEnumerable<PetDto>>.Success(pets.Select(p => p.MapToPetDto()).ToList());
    }

    public async Task<Result<PetDto?>> Execute(GetPetByIdQueryAsync query)
    {
        Pet? pet = await _dbConnection.QueryFirstOrDefaultAsync<Pet>("Usp_Pet_GetById",
            commandType: CommandType.StoredProcedure,
            param: _currentUserRepository.WithUser(query));

        if (pet == null) return Result<PetDto?>.Failure(Error.NotFound);
        return Result<PetDto?>.Success(pet.MapToPetDto());
    }

    #endregion

    #region POST

    public async Task<Result<int>> Execute(InsertPetCommandAsync command)
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

    #endregion

    #region PUT

    public async Task<Result> Execute(UpdatePetCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region PATCH

    public async Task<Result> Execute(UpdatePetPseudoCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Update_Pseudo",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeletePetCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Pet_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));

        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion
}