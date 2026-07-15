using System.Data;
using Dapper;
using PetHealth.Application.Commands.Treatments;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Treatments;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class TreatmentService : ITreatmentRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public TreatmentService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<Treatment>>> Execute(GetAllTreatmentQueryAsync query)
    {
        var treatments = (await _dbConnection.QueryAsync<Treatment>("Usp_Treatment_GetAll",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!treatments.Any()) return Result<IEnumerable<Treatment>>.Failure(Error.NotFound);
        return Result<IEnumerable<Treatment>>.Success(treatments);
    }

    public async Task<Result<IEnumerable<Treatment>>> Execute(GetAllByUserTreatmentQueryAsync query)
    {
        var treatments = (await _dbConnection.QueryAsync<Treatment>("Usp_Treatment_GetAllByUser",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!treatments.Any()) return Result<IEnumerable<Treatment>>.Failure(Error.NotFound);
        return Result<IEnumerable<Treatment>>.Success(treatments);
    }

    #endregion

    #region INSERT

    public async Task<Result> Execute(InsertTreatmentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Insert",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region UPDATE

    public async Task<Result> Execute(UpdateTreatmentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteTreatmentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion
}