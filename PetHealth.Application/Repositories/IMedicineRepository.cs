using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Medicines;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IMedicineRepository: 
    IQueryHandlerAsync<GetAllMedicineQueryAsync,Result<IEnumerable<Medicine>>>
{
    
}