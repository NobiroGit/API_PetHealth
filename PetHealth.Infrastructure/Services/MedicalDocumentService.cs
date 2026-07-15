using System.Data;
using Dapper;
using PetHealth.Application.Commands.MedicalDocuments;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.MedicalDocuments;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class MedicalDocumentService : IMedicalDocumentRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public MedicalDocumentService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<MedicalDocument>>> Execute(GetAllMedicalDocumentQueryAsync query)
    {
        var medicalDocuments = (await _dbConnection.QueryAsync<MedicalDocument>("Usp_MedicalDocument_GetAll",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();

        if (!medicalDocuments.Any()) return Result<IEnumerable<MedicalDocument>>.Failure(Error.NotFound);
        return Result<IEnumerable<MedicalDocument>>.Success(medicalDocuments);
    }

    public async Task<Result<IEnumerable<MedicalDocument>>> Execute(GetAllByUserMedicalDocumentQueryAsync query)
    {
        var medicalDocuments = (await _dbConnection.QueryAsync<MedicalDocument>("Usp_MedicalDocument_GetAllByUser",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!medicalDocuments.Any()) return Result<IEnumerable<MedicalDocument>>.Failure(Error.NotFound);
        return Result<IEnumerable<MedicalDocument>>.Success(medicalDocuments);
    }

    #endregion

    #region INSERT

    public async Task<Result> Execute(InsertMedicalDocumentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_MedicalDocument_Insert",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region UPDATE

    public async Task<Result> Execute(UpdateMedicalDocumentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_MedicalDocument_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteMedicalDocumentCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_MedicalDocument_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows >= 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion
}