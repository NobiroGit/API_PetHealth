using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.WeightRecords;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.WeightRecords;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WeightRecordController : Controller
{
    private readonly IWeightRecordRepository _weightRecordRepository;

    public WeightRecordController(IWeightRecordRepository weightRecordRepository)
    {
        _weightRecordRepository = weightRecordRepository;
    }

    #region GET

    [HttpGet("{petId:int}")]
    public async Task<ActionResult<Result<IEnumerable<WeightRecordDto>>>> GetWeightRecordByPetId(int petId)
    {
        var result = await _weightRecordRepository.Execute(new GetWeightRecordByPetIdQueryAsync(petId));
        if (!result.IsSuccess) return NotFound(result.Error);
        return Ok(result.Data);
    }

    #endregion
    
    #region POST

    [HttpPost]
    public async Task<ActionResult<Result>> InsertWeightRecordAsync([FromBody] InsertWeightRecordDto weightRecordDto)
    {
        var result = await _weightRecordRepository.Execute(new InsertWeightRecordCommandAsync(weightRecordDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }
    
    #endregion
    
    #region PUT

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result>> UpdateWeightRecordAsync([FromBody] UpdateWeightRecordDto weightRecordDto,
        int id)
    {
        var result = await _weightRecordRepository.Execute(new UpdateWeightRecordCommandAsync(weightRecordDto, id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }
    
    #endregion
    
    #region DELETE

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteWeightRecordAsync(int id)
    {
        var result = await _weightRecordRepository.Execute(new DeleteWeightRecordCommandAsync(id));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }
    
    #endregion
}