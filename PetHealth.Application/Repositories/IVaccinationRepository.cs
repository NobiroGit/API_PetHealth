using PetHealth.Application.Commands.Vaccinations;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Vaccinations;

namespace PetHealth.Application.Repositories;

public interface IVaccinationRepository: 
    IQueryHandlerAsync<GetVaccinationByPetIdQueryAsync, Result<IEnumerable<VaccinationDto>>>,
    ICommandHandlerAsync<InsertVaccinationCommandAsync, Result>,
    ICommandHandlerAsync<UpdateVaccinationCommandAsync, Result>,
    ICommandHandlerAsync<DeleteVaccinationCommandAsync, Result>
{
    
}