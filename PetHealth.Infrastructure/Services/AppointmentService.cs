using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Commands.Appointment;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Mapping;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Appointment;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class AppointmentService : IAppointmentRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public AppointmentService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<AppointmentDto>>> Execute(GetAllAppointmentQueryAsync query)
    {
        try
        {
            var appointments = (await _dbConnection.QueryAsync<Appointment>("Usp_Appointment_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).Select(i => i.MapToDto()).ToList();
            if (!appointments.Any()) return Result<IEnumerable<AppointmentDto>>.Failure(Error.NotFound);
            return Result<IEnumerable<AppointmentDto>>.Success(appointments);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<AppointmentDto>>.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<AppointmentDto>>.Failure(new Error(e));
        }
    }

    public async Task<Result<IEnumerable<AppointmentDto>>> Execute(GetByPetIdAppointmentQueryAsync query)
    {
        try
        {
            var appointments = (await _dbConnection.QueryAsync<Appointment>("usp_Appointment_GetByPetId",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).Select(i => i.MapToDto()).ToList();
            if (!appointments.Any()) return Result<IEnumerable<AppointmentDto>>.Failure(Error.NotFound);
            
            return Result<IEnumerable<AppointmentDto>>.Success(appointments);
        }
        catch (SqlException e)
        {
            return Result<IEnumerable<AppointmentDto>>.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<AppointmentDto>>.Failure(new Error(e));
        }
    }

    #endregion

    #region POST

    public async Task<Result> Execute(InsertAppointmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Appointment_Insert",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
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

    #region PUT

    public async Task<Result> Execute(UpdateAppointmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Appointment_Update",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
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

    public async Task<Result> Execute(DeleteAppointmentCommandAsync command)
    {
        try
        {
            int rows = await _dbConnection.ExecuteAsync("Usp_Appointment_Delete",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(command));
            return rows == 1 ? Result.Success() : Result.Failure(Error.NotFound);
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
}