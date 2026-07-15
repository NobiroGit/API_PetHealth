using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.Treatments;
using PetHealth.Application.Common.DTOs.TreatmentDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Treatments;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TreatmentController : Controller
{
    private readonly ITreatmentRepository _treatmentRepository;

    public TreatmentController(ITreatmentRepository treatmentRepository)
    {
        _treatmentRepository = treatmentRepository;
    }

    #region GET

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<Treatment>>>> GetAllTreatments()
    {
        Result<IEnumerable<Treatment>> treatments = await _treatmentRepository.Execute(new GetAllTreatmentQueryAsync());
        if (!treatments.IsSuccess) return BadRequest();
        return Ok(treatments.Data);
    }

    [Authorize]
    [HttpGet("User")]
    public async Task<ActionResult<Result<IEnumerable<Treatment>>>> GetAllTreatmentsByUser(DateTime date)
    {
        Result<IEnumerable<Treatment>> treatments =
            await _treatmentRepository.Execute(new GetAllByUserTreatmentQueryAsync());
        if (!treatments.IsSuccess) return BadRequest();
        return Ok(treatments.Data);
    }

    #endregion

    #region POST

    [Authorize(Roles = "Admin, Vet")]
    [HttpPost]
    public async Task<ActionResult<Result>> InsertTreatment([FromBody] InsertTreatmentDto dto)
    {
        Result result = await _treatmentRepository.Execute(new InsertTreatmentCommandAsync(dto));
        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    #endregion

    #region PUT

    [Authorize(Roles = "Admin, Vet")]
    [HttpPut]
    public async Task<ActionResult<Result>> UpdateTreatment([FromBody] UpdateTreatmentDto dto)
    {
        Result result = await _treatmentRepository.Execute(new UpdateTreatmentCommandAsync(dto));
        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    #endregion

    #region DELETE

    [Authorize(Roles = "Admin")]
    [HttpDelete("{treatmentId}")]
    public async Task<ActionResult<Result>> DeleteTreatment(int treatmentId)
    {
        Result result = await _treatmentRepository.Execute(new DeleteTreatmentCommandAsync(treatmentId));
        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    #endregion
}