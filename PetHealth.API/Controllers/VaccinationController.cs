using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.Vaccinations;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Vaccinations;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VaccinationController : Controller
{
    private readonly IVaccinationRepository _vaccinationRepository;

    public VaccinationController(IVaccinationRepository vaccinationRepository)
    {
        _vaccinationRepository = vaccinationRepository;
    }

    #region GET

    [HttpGet("{petId:int}")]
    public async Task<ActionResult<IEnumerable<VaccinationDto>>> GetVaccinationsByPetId(int petId)
    {
        Result<IEnumerable<VaccinationDto>> vaccinations = await _vaccinationRepository.Execute(new GetVaccinationByPetIdQueryAsync(petId));
        if (!vaccinations.IsSuccess)
            return NotFound(vaccinations.Error);
        return Ok(vaccinations.Data);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<ActionResult<Result>> InsertVaccination([FromBody] InsertVaccinationDto vaccinationDto)
    {
        Result result = await _vaccinationRepository.Execute(new InsertVaccinationCommandAsync(vaccinationDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion
    
    #region PUT

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result>> UpdateVaccination([FromBody] UpdateVaccinationDto vaccinationDto, int id)
    {
        Result result = await _vaccinationRepository.Execute(new UpdateVaccinationCommandAsync(vaccinationDto, id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);   
    }
    
    #endregion
    
    #region DELETE

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteVaccination(int id)
    {
        Result result = await _vaccinationRepository.Execute(new DeleteVaccinationCommandAsync(id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);  
    }
    
    #endregion
}