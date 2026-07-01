using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Commands.Prescriptions;
using PetHealth.Application.Commands.PrescriptionsItem;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Prescriptions;
using PetHealth.Application.Queries.PrescriptionsItem;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class PrescriptionService : IPrescriptionRepository, IPrescriptionItemRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public PrescriptionService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region PRESCRIPTIONS

    public async Task<Result<IEnumerable<Prescription>>> Execute(GetAllPrescriptionQueryAsync query)
    {
        try
        {
            var prescriptions = (await _dbConnection.QueryAsync<Prescription>("Usp_Prescription_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!prescriptions.Any()) return Result<IEnumerable<Prescription>>.Failure(Error.NotFound);
            return Result<IEnumerable<Prescription>>.Success(prescriptions);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<Prescription>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result<IEnumerable<Prescription>>> Execute(GetPrescriptionByIdQueryAsync query)
    {
        try
        {
            var prescriptions = (await _dbConnection.QueryAsync<Prescription>("Usp_Prescription_GetById",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!prescriptions.Any()) return Result<IEnumerable<Prescription>>.Failure(Error.NotFound);
            return Result<IEnumerable<Prescription>>.Success(prescriptions);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<Prescription>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result<int>> Execute(InsertPrescriptionCommandAsync command)
    {
        try
        {
            var parameters = new DynamicParameters(command);
            parameters.Add("NewId", DbType.Int32, direction: ParameterDirection.Output);

            int rows = await _dbConnection.ExecuteAsync("Usp_Prescription_Insert",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(parameters));
            return rows >= 1 ? Result<int>.Success(parameters.Get<int>("NewId")) : Result<int>.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result<int>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(DeletePrescriptionCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Prescription_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    #endregion

    #region PRESCRIPTION ITEMS

    public async Task<Result<IEnumerable<PrescriptionItem>>> Execute(GetAllPrescriptionItemQueryAsync query)
    {
        try
        {
            var prescriptionsItem = (await _dbConnection.QueryAsync<PrescriptionItem>("Usp_PrescriptionItem_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!prescriptionsItem.Any()) return Result<IEnumerable<PrescriptionItem>>.Failure(Error.NotFound);
            return Result<IEnumerable<PrescriptionItem>>.Success(prescriptionsItem);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<PrescriptionItem>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result<IEnumerable<PrescriptionItem>>> Execute(GetPrescriptionItemByIdQueryAsync query)
    {
        try
        {
            var prescriptionsItem = (await _dbConnection.QueryAsync<PrescriptionItem>("Usp_PrescriptionItem_GetById",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            if (!prescriptionsItem.Any()) return Result<IEnumerable<PrescriptionItem>>.Failure(Error.NotFound);
            return Result<IEnumerable<PrescriptionItem>>.Success(prescriptionsItem);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<PrescriptionItem>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(InsertPrescriptionItemCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_PrescriptionItem_Insert",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(UpdatePrescriptionItemCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_PrescriptionItem_Update",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(DeletePrescriptionItemCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_PrescriptionItem_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    #endregion
}