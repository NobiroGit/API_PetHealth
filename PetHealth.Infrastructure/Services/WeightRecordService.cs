using System.Data;
using Dapper;
using PetHealth.Application.Commands.WeightRecords;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.WeightRecords;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class WeightRecordService : IWeightRecordRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public WeightRecordService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<WeightRecordDto>>> Execute(GetWeightRecordByPetIdQueryAsync query)
    {
        var weightRecords =
            (await _dbConnection.QueryAsync<WeightRecord>("Usp_WeightRecord_GetByPetId",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!weightRecords.Any()) return Result<IEnumerable<WeightRecordDto>>.Failure(Error.NotFound);
        return Result<IEnumerable<WeightRecordDto>>.Success(weightRecords.Select(i => i.ToWeightRecordDto()));
    }

    #endregion

    #region POST

    public async Task<Result> Execute(InsertWeightRecordCommandAsync command)
    {
        var parameters = new DynamicParameters(command);
        parameters.Add("@WeightKg", command.WeightKg, DbType.Decimal, precision: 6, scale: 3);

        int rows = await _dbConnection.ExecuteAsync("Usp_WeightRecord_Insert",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(parameters));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region PUT

    public async Task<Result> Execute(UpdateWeightRecordCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_WeightRecord_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteWeightRecordCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_WeightRecord_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion
}