namespace IASales.Api.DTOs;

public record CreateCampaingDTO(
    string Name,
    string Plataform,
    string Objective,
    decimal BudgetDaily,
    bool GenerateWithAI = false,
    Guid? ProductId = null
);
