using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Commands.Pets;
using PetHealth.Application.Queries.Pets;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PetController : ControllerBase
{
    private readonly IPetRepository _petRepository;

    public PetController(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    #region GET

    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetAllPetsAsync()
    {
        Result<IEnumerable<PetDto>> pets = await _petRepository.Execute(new GetAllPetsQueryAsync());
        if (!pets.IsSuccess)
            return StatusCode(StatusCodes.Status500InternalServerError, pets.Error);
        return Ok(pets.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetDto>> GetPetByIdAsync(int id)
    {
        Result<PetDto?> pet = await _petRepository.Execute(new GetPetByIdQueryAsync(id));

        if (!pet.IsSuccess) return NotFound(pet.Error);

        return Ok(pet.Data);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<ActionResult<int>> InsertPetAsync([FromBody] InsertPetDto petDto)
    {
        Result<int> id = await _petRepository.Execute(new InsertPetCommandAsync(petDto));
        
        if(!id.IsSuccess) 
            return BadRequest(id.Error);
        
        Result<PetDto?> pet = await _petRepository.Execute(new GetPetByIdQueryAsync(id.Data));
        
        return CreatedAtAction(nameof(GetPetByIdAsync), new { id }, pet.Data);
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePetAsync(int id)
    {
        Result<PetDto?> pet = await _petRepository.Execute(new GetPetByIdQueryAsync(id));
        if (pet.Data == null) return NotFound();

        Result result = await _petRepository.Execute(new DeletePetCommandAsync(id));
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    #endregion

    #region PATCH

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> UpdatePetAdminAsync([FromBody] UpdatePetAdminDto adminDto, int id)
    {
        Result<PetDto?> pet = await _petRepository.Execute(new GetPetByIdQueryAsync(id));

        if (!pet.IsSuccess) 
            return NotFound(pet.Error);

        Result updated = await _petRepository.Execute(new UpdatePetCommandAsync(adminDto, id));
        if (!updated.IsSuccess)
            return BadRequest(updated.Error);

        return Ok(updated);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Result>> UpdatePetPseudoAsync([FromBody] UpdatePetPseudoDto pseudoDto, int id)
    {
        Result<PetDto?> pet = await _petRepository.Execute(new GetPetByIdQueryAsync(id));
        if (!pet.IsSuccess)
            return NotFound(pet.Error);

        Result updated = await _petRepository.Execute(new UpdatePetPseudoCommandAsync(pseudoDto.Pseudo, id));
        if (!updated.IsSuccess)
            return BadRequest(updated.Error);

        return Ok(updated);
    }

    #endregion
}