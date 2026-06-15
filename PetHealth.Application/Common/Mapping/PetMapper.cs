using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class PetMapper
{
    public static PetDto MapToPetDto(this Pet pet)
    {
        return new PetDto()
        {
            Id = pet.Id,
            OwnerId = pet.OwnerId,
            Name = pet.Name,
            Pseudo = pet.Pseudo,
            Species = pet.Species,
            Breed = pet.Breed,
            Gender = pet.Gender,
            BirthDate = pet.BirthDate,
            MicrochipNumber = pet.MicrochipNumber,
            PhotoUrl = pet.PhotoUrl,
            CreatedAt = pet.CreatedAt
        };
    }
}