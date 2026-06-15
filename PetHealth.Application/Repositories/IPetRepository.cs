using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.Pets;
using PetHealth.Application.Queries.Pets;


namespace PetHealth.Application.Repositories;

public interface IPetRepository: 
    IQueryHandlerAsync<GetAllPetsQueryAsync, Result<IEnumerable<PetDto>>>,
    IQueryHandlerAsync<GetPetByIdQueryAsync, Result<PetDto?>>,
    ICommandHandlerAsync<InsertPetCommandAsync, Result<int>>,
    ICommandHandlerAsync<DeletePetCommandAsync, Result>,
    ICommandHandlerAsync<UpdatePetCommandAsync, Result>,
    ICommandHandlerAsync<UpdatePetPseudoCommandAsync, Result>
{
    
}