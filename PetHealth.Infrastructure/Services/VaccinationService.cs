using System.Data;
using Dapper;
using PetHealth.Application.Commands.Vaccinations;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Vaccinations;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class VaccinationService : IVaccinationRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public VaccinationService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<VaccinationDto>>> Execute(GetVaccinationByPetIdQueryAsync query)
    {
        var vaccinations = (await _dbConnection.QueryAsync<Vaccination>("Usp_Vaccination_GetByPetId",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
        if (!vaccinations.Any())
            return Result<IEnumerable<VaccinationDto>>.Failure(Error.NotFound);
        return Result<IEnumerable<VaccinationDto>>.Success(vaccinations.Select(i => i.ToVaccinationDto()));
    }

    #endregion

    #region POST

    public async Task<Result> Execute(InsertVaccinationCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Insert",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region PUT

    public async Task<Result> Execute(UpdateVaccinationCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Update",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion

    #region DELETE

    public async Task<Result> Execute(DeleteVaccinationCommandAsync command)
    {
        int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Delete",
            commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
        return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
    }

    #endregion
}