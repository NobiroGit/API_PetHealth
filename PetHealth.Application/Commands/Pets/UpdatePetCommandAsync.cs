using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Pets;

public class UpdatePetCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public int? OwnerId { get; init; }
    public string? Name { get; init; }
    public string? Species { get; init; }
    public string? Breed { get; init; }
    public DateOnly? BirthDate { get; init; }
    public char? Gender { get; init; } // 'M' / 'F' / 'U'
    public string? MicrochipNumber { get; init; }

    public UpdatePetCommandAsync(UpdatePetAdminDto adminDto, int id)
    {
        Id = id;
        OwnerId = adminDto.OwnerId;
        Name = adminDto.Name;
        Species = adminDto.Species;
        Breed = adminDto.Breed;
        BirthDate = adminDto.BirthDate;
        Gender = adminDto.Gender;
        MicrochipNumber = adminDto.MicrochipNumber;
        
    }
}