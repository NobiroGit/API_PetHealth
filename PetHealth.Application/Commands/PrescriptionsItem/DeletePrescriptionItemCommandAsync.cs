using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.PrescriptionsItem;

public class DeletePrescriptionItemCommandAsync : ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeletePrescriptionItemCommandAsync(int id)
    {
        Id = id;
    }
}