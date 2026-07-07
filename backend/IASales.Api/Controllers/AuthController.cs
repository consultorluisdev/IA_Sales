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

    // login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _auth.LoginAsync(dto);
        return result == null ? Unauthorized(new  { message = "Credenciais inválidas."}) : Ok(result);
    }

    // register 
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        var result = await _auth.RegisterAsync(dto);
        return result == null 
        ? BadRequest( new { message ="Email já cadastrado."}) 
        : Ok( new { message = "Usuário registrado com sucesso."});
    }
}