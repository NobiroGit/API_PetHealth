using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Pets;

public class DeletePetCommandAsync: ICommandDefinitionAsync<Result>
{
    public DeletePetCommandAsync(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}