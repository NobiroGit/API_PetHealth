using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.AppUsers;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserController : ControllerBase
{
    private IAppUserRepository _appUserRepository;

    public AppUserController(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    #region GET

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<AppUserDto>>>> GetAllAppUserAsync()
    {
        var appUsers = await _appUserRepository.Execute(new GetAllAppUserAsync());
        if (!appUsers.IsSuccess) return NotFound(appUsers.Error);
        return Ok(appUsers.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<AppUserDto>>> GetAppUserByIdAsync(int id)
    {
        var appUser = await _appUserRepository.Execute(new GetAppUserByIdAsync(id));
        if (!appUser.IsSuccess) return NotFound(appUser.Error);
        return Ok(appUser.Data);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<ActionResult<Result<int>>> InsertAppUserAsync([FromBody] InsertAppUserDto appUser)
    {
        Result<int> id = await _appUserRepository.Execute(new InsertAppUserCommandAsync(appUser));
        if (!id.IsSuccess) return BadRequest(id.Error);

        Result<AppUserDto?> appUserResult = await _appUserRepository.Execute(new GetAppUserByIdAsync(id.Data));
        return CreatedAtAction(nameof(GetAppUserByIdAsync), new { id }, appUserResult.Data);
    }

    #endregion

    #region PUT

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> UpdateAppUserAsync([FromBody] UpdateAppUserDto appUser, int id)
    {
        Result result = await _appUserRepository.Execute(new UpdateAppUserCommandAsync(appUser, id));
        if (!result.IsSuccess) return BadRequest(result.Error);

        return Ok(result);
    }

    #endregion

    #region PATCH

    [HttpPatch("email/")]
    public async Task<ActionResult<Result>> UpdateEmailAppUserAsync([FromBody] UpdateEmailAppUserDto emailDto)
    {
        Result result = await _appUserRepository.Execute(new UpdateEmailAppUserCommandAsync(emailDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpPatch("password/")]
    public async Task<ActionResult<Result>> UpdatePasswordAppUserAsync([FromBody] UpdatePasswordAppUserDto passwordDto)
    {
        Result result = await _appUserRepository.Execute(new UpdatePasswordAppUserCommandAsync(passwordDto));
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result);
    }

    #endregion

    #region DELETE

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