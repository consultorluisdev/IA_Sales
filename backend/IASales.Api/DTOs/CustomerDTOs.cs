namespace IASales.Api.DTOs;

public record CreateCustomerDTO(
    string Name,
    string? Email,
    string? Phone,
    string? Photo,
    string Source,
    List<string> Interests,
    string? Notes
);

public record UpdateCustomerDTO(
    string? Name,
    string? Email,
    string? Phone,
    string? Notes
);