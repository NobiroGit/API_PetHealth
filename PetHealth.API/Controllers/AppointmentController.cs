using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.Appointment;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Appointment;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentController : Controller
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    #region GET

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<AppointmentDto>>>> GetAllAppointments()
    {
        Result<IEnumerable<AppointmentDto>> result =
            await _appointmentRepository.Execute(new GetAllAppointmentQueryAsync());
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    [Authorize]
    [HttpGet("{petId:int}")]
    public async Task<ActionResult<Result<IEnumerable<AppointmentDto>>>> GetAppointmentsByPetId(int petId)
    {
        Result<IEnumerable<AppointmentDto>> result =
            await _appointmentRepository.Execute(new GetByPetIdAppointmentQueryAsync(petId));
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    #endregion

    #region POST

    [Authorize(Roles = "Admin, Vet")]
    [HttpPost]
    public async Task<ActionResult<Result>> InsertAppointment([FromBody] InsertAppointmentDto appointmentDto)
    {
        Result result = await _appointmentRepository.Execute(new InsertAppointmentCommandAsync(appointmentDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region PUT

    [Authorize(Roles = "Admin, Vet")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result>> UpdateAppointment([FromBody] UpdateAppointmentDto appointmentDto, int id)
    {
        Result result = await _appointmentRepository.Execute(new UpdateAppointmentCommandAsync(appointmentDto, id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region DELETE

    [Authorize(Roles = "Admin, Vet")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteAppointment(int id)
    {
        Result result = await _appointmentRepository.Execute(new DeleteAppointmentCommandAsync(id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion
}