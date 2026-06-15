using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Pets;

public class UpdatePetPseudoCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string? Pseudo
    {
        get;
        set
        {
            field = value;
            if(field?.Trim() == string.Empty) field = null;
        }
    }

    public UpdatePetPseudoCommandAsync(string? pseudo, int id)
    {
        Id = id;
        Pseudo = pseudo;
    }
    
}