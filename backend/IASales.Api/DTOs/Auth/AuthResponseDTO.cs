namespace IASales.Api.DTOs;

public record UserDTO(Guid Id, string Name, string Email, string Role);

public record AuthResponseDTO(string Token, UserDTO User);