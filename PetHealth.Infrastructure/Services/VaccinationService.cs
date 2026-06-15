using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Commands.Vaccinations;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Vaccinations;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class VaccinationService: IVaccinationRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public VaccinationService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }
    
    public async Task<Result<IEnumerable<VaccinationDto>>> Execute(GetVaccinationByPetIdQueryAsync query)
    {
        try
        {
            var vaccinations = (await _dbConnection.QueryAsync<Vaccination>("Usp_Vaccination_GetByPetId", commandType:CommandType.StoredProcedure, param:_currentUserRepository.WithUser(query))).ToList();
            if (!vaccinations.Any())
                return Result<IEnumerable<VaccinationDto>>.Failure(Error.NotFound);
            return Result<IEnumerable<VaccinationDto>>.Success(vaccinations.Select(i => i.ToVaccinationDto()));
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<VaccinationDto>>.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(InsertVaccinationCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Insert", commandType:CommandType.StoredProcedure, param:_currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }


    public async Task<Result> Execute(UpdateVaccinationCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Update", commandType:CommandType.StoredProcedure, param:_currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }

    public async Task<Result> Execute(DeleteVaccinationCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Vaccination_Delete", commandType:CommandType.StoredProcedure, param:_currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
        }
        catch (SqlException e)
        {
            return Result.Failure(new Error(e.ToError()));
        }
    }
}