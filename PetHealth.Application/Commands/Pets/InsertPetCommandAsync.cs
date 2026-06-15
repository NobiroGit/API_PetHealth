using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Pets;

public class InsertPetCommandAsync: ICommandDefinitionAsync<Result<int>>
{
    public int OwnerId { get; init; }
    public string Name { get; init; } 
    public string Species { get; init; }
    public string Breed { get; init; }
    public DateOnly BirthDate { get; init; }
    public char Gender { get; init; } // 'M' / 'F' / 'U'
    public string MicrochipNumber { get; init; }
    
    public InsertPetCommandAsync(InsertPetDto p)
    {
        OwnerId = p.OwnerId;
        Name = p.Name;
        Species = p.Species;
        Breed = p.Breed;
        BirthDate = p.BirthDate;
        Gender = p.Gender;
        MicrochipNumber = p.MicrochipNumber; 
    }
}