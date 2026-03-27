using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using IASales.Api.Data;
using IASales.Api.DTOs;
using IASales.Api.Entities;

namespace IASales.Api.Services;

public class AuthService
{
    private readonly AppDbContext _ctx;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext ctx, IConfiguration config)
    {
        _ctx = ctx;
        _config = config;
    }
    public async Task<AuthResponseDTO?> LoginAsync(LoginDTO dto)
    {
        var user = await _ctx.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        return null;

        var token = GenerateJwt(user);

        return new AuthResponseDTO(
            token, 
            new UserDTO(user.Id, user.Name, user.Email, user.Role)
        );
    }
    public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO dto)
    {
        var exists = await _ctx.Users.AnyAsync(u => u.Email == dto.Email);
        if(exists) return null;

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "seller"
        };
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();

        var token = GenerateJwt(user);

        return new AuthResponseDTO(
            token, 
            new UserDTO(user.Id, user.Name, user.Email, user.Role)
        );
    }
    private string GenerateJwt(User user)
    {
        var secret = _config["JWT:Secret"] ?? throw new InvalidOperationException("JWT:Secret não configurado");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}