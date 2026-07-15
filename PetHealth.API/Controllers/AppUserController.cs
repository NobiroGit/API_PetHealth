using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.AppUsers;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class AppUserController : Controller
{
    private IAppUserRepository _appUserRepository;

    public AppUserController(IAppUserRepository appUserRepository, ILogger<AppUserController> logger)
    {
        _appUserRepository = appUserRepository;
    }

    #region GET
    
    [HttpGet]
    [Authorize(Roles = "Admin, Vet")]
    public async Task<ActionResult<Result<IEnumerable<AppUserDto>>>> GetAllAppUserAsync()
    {
        Result<IEnumerable<AppUserDto>> appUsers = await _appUserRepository.Execute(new GetAllAppUserAsync());
        if (!appUsers.IsSuccess) return NotFound(appUsers.Error);
        return Ok(appUsers.Data);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Vet")]
    public async Task<ActionResult<Result<AppUserDto>>> GetAppUserByIdAsync(int id)
    {
        Result<AppUserDto?> appUser = await _appUserRepository.Execute(new GetAppUserByIdAsync(id));
        if (!appUser.IsSuccess) return NotFound(appUser.Error);
        return Ok(appUser.Data);
    }

    #endregion

    #region POST

    //PEUT ÊTRE FAIRE UNE METHODE POUR AJOUTER UN VET POUR LES ADMINS ET VET EXISTANT
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Result<int>>> InsertVetAppUserAsync([FromBody] InsertVetAppUserDto vetAppUser)
    {
        if (!ModelState.IsValid)
            return new BadRequestObjectResult(ModelState);
        
        Result<int> id = await _appUserRepository.Execute(new InsertVetAppUserCommandAsync(vetAppUser));
        if (!id.IsSuccess) return BadRequest(id.Error);

        Result<AppUserDto?> appUserResult = await _appUserRepository.Execute(new GetAppUserByIdAsync(id.Data));
        return CreatedAtAction(nameof(GetAppUserByIdAsync), new { id }, appUserResult.Data);
    }

    #endregion

    #region PUT

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> UpdateAppUserAsync([FromBody] UpdateAppUserDto appUser, int id)
    {
        Result result = await _appUserRepository.Execute(new UpdateAppUserCommandAsync(appUser, id));
        if (!result.IsSuccess) return BadRequest(result.Error);

        return Ok(result);
    }

    #endregion

    #region PATCH
    [Authorize]
    [HttpPatch("email/")]
    public async Task<ActionResult<Result>> UpdateEmailAppUserAsync([FromBody] UpdateEmailAppUserDto emailDto)
    {
        Result result = await _appUserRepository.Execute(new UpdateEmailAppUserCommandAsync(emailDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    [Authorize]
    [HttpPatch("password/")]
    public async Task<ActionResult<Result>> UpdatePasswordAppUserAsync([FromBody] UpdatePasswordAppUserDto passwordDto)
    {
        Result result = await _appUserRepository.Execute(new UpdatePasswordAppUserCommandAsync(passwordDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region DELETE

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> DeleteAppUserAsync(int id)
    {
        Result result = await _appUserRepository.Execute(new DeleteAppUserCommandAsync(id));
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }

    #endregion
}