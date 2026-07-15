using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.Prescriptions;
using PetHealth.Application.Commands.PrescriptionsItem;
using PetHealth.Application.Common.DTOs.PrescriptionDto;
using PetHealth.Application.Common.DTOs.PrescriptionItemDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Prescriptions;
using PetHealth.Application.Queries.PrescriptionsItem;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PrescriptionController : Controller
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPrescriptionItemRepository _prescriptionItemRepository;

    public PrescriptionController(IPrescriptionRepository prescriptionRepository,
        IPrescriptionItemRepository prescriptionItemRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _prescriptionItemRepository = prescriptionItemRepository;
    }

    #region GET

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<Prescription>>>> GetAllPrescriptions()
    {
        Result<IEnumerable<Prescription>> result =
            await _prescriptionRepository.Execute(new GetAllPrescriptionQueryAsync());
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<IEnumerable<Prescription>>>> GetPrescriptionById(int id)
    {
        Result<IEnumerable<Prescription>> result =
            await _prescriptionRepository.Execute(new GetPrescriptionByIdQueryAsync(id));
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet("Item")]
    public async Task<ActionResult<Result<IEnumerable<PrescriptionItem>>>> GetAllPrescriptionItems()
    {
        Result<IEnumerable<PrescriptionItem>> result =
            await _prescriptionItemRepository.Execute(new GetAllPrescriptionItemQueryAsync());
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin, Vet")]
    [HttpGet("Item/{prescriptionId}")]
    public async Task<ActionResult<Result<IEnumerable<PrescriptionItem>>>> GetPrescriptionItemsByPrescriptionId(
        int prescriptionId)
    {
        Result<IEnumerable<PrescriptionItem>> result =
            await _prescriptionItemRepository.Execute(new GetPrescriptionItemByIdQueryAsync(prescriptionId));
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    #endregion

    #region POST

    [Authorize(Roles = "Admin, Vet")]
    [HttpPost]
    public async Task<ActionResult<Result<int>>> InsertPrescription([FromBody] InsertPrescriptionDto prescriptionDto)
    {
        Result<int> result = await _prescriptionRepository.Execute(new InsertPrescriptionCommandAsync(prescriptionDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    [Authorize(Roles = "Admin, Vet")]
    [HttpPost("Item")]
    public async Task<ActionResult<Result>> InsertPrescriptionItem(
        [FromBody] InsertPrescriptionItemDto prescriptionItemDto)
    {
        Result result =
            await _prescriptionItemRepository.Execute(new InsertPrescriptionItemCommandAsync(prescriptionItemDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region PUT

    [Authorize(Roles = "Admin, Vet")]
    [HttpPut]
    public async Task<ActionResult<Result>> UpdatePrescriptionItem(
        [FromBody] UpdatePrescriptionItemDto prescriptionItemDto)
    {
        Result result =
            await _prescriptionItemRepository.Execute(new UpdatePrescriptionItemCommandAsync(prescriptionItemDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region DELETE

    [Authorize(Roles = "Admin, Vet")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> DeletePrescription(int id)
    {
        Result result = await _prescriptionRepository.Execute(new DeletePrescriptionCommandAsync(id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    [Authorize(Roles = "Admin, Vet")]
    [HttpDelete("Item/{id}")]
    public async Task<ActionResult<Result>> DeletePrescriptionItem(int id)
    {
        Result result = await _prescriptionItemRepository.Execute(new DeletePrescriptionItemCommandAsync(id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion
}