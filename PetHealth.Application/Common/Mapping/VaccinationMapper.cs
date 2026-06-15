using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class VaccinationMapper
{
    public static VaccinationDto ToVaccinationDto(this Vaccination vaccination)
    {
        return new VaccinationDto()
        {
            Id = vaccination.Id,
            PetId = vaccination.PetId,
            VaccineName = vaccination.VaccineName,
            VaccinationDate = vaccination.VaccinationDate,
            NextBoosterDate = vaccination.NextBoosterDate,
            BatchNumber = vaccination.BatchNumber
        };
    }
}