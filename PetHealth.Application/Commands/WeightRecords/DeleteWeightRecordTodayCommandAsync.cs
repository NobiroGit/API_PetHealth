using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.WeightRecords;

public class DeleteWeightRecordTodayCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    
    public DeleteWeightRecordTodayCommandAsync(int id)
    {
        this.Id = id;
    }
}