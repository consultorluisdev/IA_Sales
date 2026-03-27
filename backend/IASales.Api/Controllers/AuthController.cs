using Microsoft.AspNetCore.Mvc;
using IASales.Api.DTOs;
using IASales.Api.Services;

namespace IASales.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _auth.LoginAsync(dto);
        return result == null ? Unauthorized("Credenciais inválidas.") : Ok(result);
    } 
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        var result = await _auth.RegisterAsync(dto);
        return result == null ? BadRequest("Email já cadastrado.") : Ok(result);
    }
}