using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Prescriptions;

public class DeletePrescriptionCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeletePrescriptionCommandAsync(int id)
    {
        Id = id;
    }
}