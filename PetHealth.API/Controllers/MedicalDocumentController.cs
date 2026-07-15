using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.MedicalDocuments;
using PetHealth.Application.Common.DTOs.MedicalDocumentDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.MedicalDocuments;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MedicalDocumentController : Controller
{
    private readonly IMedicalDocumentRepository _medicalDocumentRepository;

    public MedicalDocumentController(IMedicalDocumentRepository medicalDocumentRepository)
    {
        _medicalDocumentRepository = medicalDocumentRepository;
    }

    #region GET
    [Authorize(Roles = "Admin, Vet")]
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<MedicalDocument>>>> GetAllMedicalDocuments()
    {
        Result<IEnumerable<MedicalDocument>> medicalDocuments =
            await _medicalDocumentRepository.Execute(new GetAllMedicalDocumentQueryAsync());
        if (!medicalDocuments.IsSuccess) return BadRequest();
        return Ok(medicalDocuments.Data);
    }

    #endregion

    #region POST
    [Authorize(Roles = "Admin, Vet")]
    [HttpPost]
    public async Task<ActionResult<Result>> InsertMedicalDocuments([FromBody] InsertMedicalDocumentDto dto)
    {
        Result result = await _medicalDocumentRepository.Execute(new InsertMedicalDocumentCommandAsync(dto));
        if (!result.IsSuccess) return BadRequest();
        return Ok(result);
    }

    #endregion

    #region PUT
    [Authorize(Roles = "Admin, Vet")]
    [HttpPut]
    public async Task<ActionResult<Result>> UpdateMedicalDocument([FromBody] UpdateMedicalDocumentDto dto)
    {
        Result result = await _medicalDocumentRepository.Execute(new UpdateMedicalDocumentCommandAsync(dto));
        if (!result.IsSuccess) return BadRequest();
        return Ok(result);
    }

    #endregion

    #region DELETE
    [Authorize(Roles = "Admin")]
    [HttpDelete("{medicalDocId}")]
    public async Task<ActionResult<Result>> DeletaMedicalDocument(int medicalDocId)
    {
        Result result = await _medicalDocumentRepository.Execute(new DeleteMedicalDocumentCommandAsync(medicalDocId));
        if (!result.IsSuccess) return BadRequest();
        return Ok(result);
    }

    #endregion
}