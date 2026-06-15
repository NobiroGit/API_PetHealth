using PetHealth.Application.Commands.WeightRecords;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.WeightRecords;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IWeightRecordRepository:
    IQueryHandlerAsync<GetWeightRecordByPetIdQueryAsync, Result<IEnumerable<WeightRecordDto>>>,
    ICommandHandlerAsync<InsertWeightRecordCommandAsync, Result>,
    ICommandHandlerAsync<UpdateWeightRecordCommandAsync, Result>,
    ICommandHandlerAsync<DeleteWeightRecordCommandAsync, Result>
{
    
}