using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHealth.Application.Commands.AppUsers;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Repositories;
using PetHealth.Application.Security;

namespace API_PetHealth.Controllers;

[Route("Login")]
[ApiController]
public class LoginController : Controller
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IOptions<JwtSettings> _jwtSettings;

    public LoginController(IAppUserRepository appUserRepository, IOptions<JwtSettings> jwtSettings)
    {
        _appUserRepository = appUserRepository;
        _jwtSettings = jwtSettings;
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginAsync([FromBody] LoginAppUserDto dto)
    {
        Result<JwtInfoAppUserDto> result = await _appUserRepository.Execute(new LoginAppUserCommandAsync(dto));
        if (!result.IsSuccess)
            return NotFound();

        //Création du token

        var issuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey));

        var scredentials = new SigningCredentials(
            issuerSigningKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, result.Data.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, result.Data.FirstName),
            new Claim(JwtRegisteredClaimNames.Email, result.Data.Email),
            new Claim("Role", result.Data.Role)
        };

        //Génération du token

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _jwtSettings.Value.Issuer,
            audience: _jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.Value.ExpirationInMinutes),
            signingCredentials: scredentials
        );

        string stringToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(stringToken);
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterAppUserDto dto)
    {
        Result result = await _appUserRepository.Execute(new RegisterAppUserCommandAsync(dto));
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }
    
    //Ajouter le logout
    [Authorize]
    [HttpPost("Logout")]
    public async Task<ActionResult> LogoutAsync()
    {
        return Ok();
    }
}