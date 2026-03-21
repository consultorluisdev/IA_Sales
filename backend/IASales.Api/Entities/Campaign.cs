using Microsoft.VisualStudio.TextTemplating;

namespace IASales.Api.Entities;

public class Campaing
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Objective { get; set; } = "sales";
    public decimal BudgetDaily { get; set; }
    public string Status { get; set; } = "draft";
    public bool IAGenerated { get; set; } = false;
    public string? Adcopy { get; set; }
    public string? Headline { get; set; }
    public decimal Spend { get; set; } = 0;
    public decimal Roas { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}