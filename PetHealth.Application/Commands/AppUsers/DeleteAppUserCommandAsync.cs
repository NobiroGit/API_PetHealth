using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class DeleteAppUserCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeleteAppUserCommandAsync(int id)
    {
        Id = id;
    }
}