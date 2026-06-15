using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Vaccinations;

public class InsertVaccinationCommandAsync: ICommandDefinitionAsync<Result>
{
    public int PetId { get; init; }
    public string VaccineName { get; init; }
    public DateOnly VaccinationDate { get; init; }
    public DateOnly? NextBoosterDate { get; init; }
    public string BatchNumber { get; init; }

    public InsertVaccinationCommandAsync(InsertVaccinationDto dto)
    {
        PetId = dto.PetId;
        VaccineName = dto.VaccineName;
        VaccinationDate = dto.VaccinationDate;
        NextBoosterDate = dto.NextBoosterDate;
        BatchNumber = dto.BatchNumber;
    }
}