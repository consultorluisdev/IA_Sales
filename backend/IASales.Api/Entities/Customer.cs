using System.Security.Permissions;

namespace IASales.Api.Entities;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Source { get; set; } = "manual";

    public List<string> Interests { get; set; } = new();

    public decimal TotalSpent { get; set; } = 0;

    public int OrderCount { get; set; } = 0;
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Tenant Tenant { get; set;} = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>(); 
}