using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Commands.AppUserRoles;
using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.AppUserRoles;
using PetHealth.Application.Repositories;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]

//Pour chaque service je dois vérifier que l'utilisateur est admin'

public class AppUserRoleController : Controller
{
    private readonly IAppUserRoleRepository _appUserRoleRepository;

    public AppUserRoleController(IAppUserRoleRepository appUserRoleRepository)
    {
        _appUserRoleRepository = appUserRoleRepository;
    }

    #region GET
    
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<AppUserRoleDto>>>> GetAllAppUserRoles()
    {
        Result<IEnumerable<AppUserRoleDto>> appUserRoles = await _appUserRoleRepository.Execute(new GetAllAppUserRoleQueriesAsync());
        if (!appUserRoles.IsSuccess)
            return BadRequest(appUserRoles.Error);
        return Ok(appUserRoles.Data);
    }
    
    #endregion
    
    #region POST

    [HttpPost]
    public async Task<ActionResult<Result>> AssignAppUserRole([FromBody]AppUserRoleDto dto)
    {
        Result result = await _appUserRoleRepository.Execute(new AssignUserRoleCommandAsync(dto));
        if (!result.IsSuccess)
            return  BadRequest(result.Error);
        return Ok(result);
    }
    
    #endregion
    
    #region DELETE

    [HttpDelete]
    public async Task<ActionResult<Result>> DeleteAppUserRole([FromBody]AppUserRoleDto dto)
    {
        Result result = await _appUserRoleRepository.Execute(new RemoveUserRoleCommandAsync(dto));
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result);
    }
    
    #endregion
}