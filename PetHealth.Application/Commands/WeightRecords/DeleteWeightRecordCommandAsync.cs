using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.WeightRecords;

public class DeleteWeightRecordCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeleteWeightRecordCommandAsync(int id)
    {
        Id = id;
    }
}