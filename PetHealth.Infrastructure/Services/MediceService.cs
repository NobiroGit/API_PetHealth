using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Medicines;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;
using PetHealth.Infrastructure.Extensions;

namespace PetHealth.Infrastructure.Services;

public class MediceService : IMedicineRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ICurrentUserRepository _currentUserRepository;

    public MediceService(IDbConnection dbConnection, ICurrentUserRepository currentUserRepository)
    {
        _dbConnection = dbConnection;
        _currentUserRepository = currentUserRepository;
        _dbConnection.Open();
    }

    #region GET

    public async Task<Result<IEnumerable<Medicine>>> Execute(GetAllMedicineQueryAsync query)
    {
        try
        {
            var medicines = (await _dbConnection.QueryAsync<Medicine>("Usp_Medicine_GetAll",
                commandType: CommandType.StoredProcedure, param: _currentUserRepository.WithUser(query))).ToList();
            return medicines.Any()
                ? Result<IEnumerable<Medicine>>.Success(medicines)
                : Result<IEnumerable<Medicine>>.Failure(Error.NotFound);

        }
        catch (SqlException e)
        {
            return Result<IEnumerable<Medicine>>.Failure(new Error(e.ToError()));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Medicine>>.Failure(new Error(e));
        }
    }

    #endregion
}