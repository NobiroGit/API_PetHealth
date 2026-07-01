using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Treatments;

public class DeleteTreatmentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeleteTreatmentCommandAsync(int id)
    {
        Id = id;
    }
}