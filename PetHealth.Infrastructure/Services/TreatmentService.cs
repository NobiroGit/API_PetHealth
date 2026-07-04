using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
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
        try
        {
            var treatments = (await _dbConnection.QueryAsync<Treatment>("Usp_Treatment_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!treatments.Any()) return Result<IEnumerable<Treatment>>.Failure(Error.NotFound);
            return Result<IEnumerable<Treatment>>.Success(treatments);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<Treatment>>.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Treatment>>.Failure(new Error(e));
        }
    }

    public async Task<Result<IEnumerable<Treatment>>> Execute(GetAllByUserTreatmentQueryAsync query)
    {
        try
        {
            var treatments = (await _dbConnection.QueryAsync<Treatment>("Usp_Treatment_GetAllByUser",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!treatments.Any()) return Result<IEnumerable<Treatment>>.Failure(Error.NotFound);
            return Result<IEnumerable<Treatment>>.Success(treatments);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<Treatment>>.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Treatment>>.Failure(new Error(e));
        }   
    }

    #endregion

    #region INSERT

    public async Task<Result> Execute(InsertTreatmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Insert",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result.Failure(new Error(e));
        }  
    }

    #endregion

    #region UPDATE

    public async Task<Result> Execute(UpdateTreatmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Update",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result.Failure(new Error(e));
        } 
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteTreatmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Treatment_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(Error.NotFound);
        }
        catch (Exception e)
        {
            return Result.Failure(new Error(e));
        }
    }

    #endregion
}