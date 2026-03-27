namespace IASales.Api.DTOs;

public record LoginDTO(string Email, string Password);
public record RegisterDTO(string Name, string Email, string Password);
public record AuthResponseDTO(string Token, UserDTO User);
public record UserDTO(Guid Id, string Name, string Email, string Role);