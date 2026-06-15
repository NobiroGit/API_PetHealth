using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Vaccinations;

public class UpdateVaccinationCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public int PetId { get; init; }
    public string VaccineName { get; init; }
    public DateOnly VaccinationDate { get; init; }
    public DateOnly? NextBoosterDate { get; init; }
    public string BatchNumber { get; init; }

    public UpdateVaccinationCommandAsync(UpdateVaccinationDto dto,int id)
    {
        Id = id;
        PetId = dto.PetId;
        VaccineName = dto.VaccineName;
        VaccinationDate = dto.VaccinationDate;
        NextBoosterDate = dto.NextBoosterDate;
        BatchNumber = dto.BatchNumber;
    }
}