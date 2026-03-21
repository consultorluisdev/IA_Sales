namespace IASales.Api.Entities;

public class VirtualTryOn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? TenantId { get; set; }
    public Guid? CustomerId { get; set; }

    public string? InputImageUrl { get; set; }
    public string? OutputImageUrl { get; set; }
    public string? RecommendedSize { get; set; }
    public string? StyleSuggestion { get; set; }
    public bool Converted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}