using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Vaccinations;

public class DeleteVaccinationCommandAsync :ICommandDefinitionAsync<Result>
{
    public int Id { get; set; }

    public DeleteVaccinationCommandAsync(int id)
    {
        Id = id;
    }
}