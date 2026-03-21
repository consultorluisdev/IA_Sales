namespace IASales.Api.Entities;

public class Order
{
    public Guid Id {get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Guid? CustomerId { get; set; }

    public string Status { get; set; } = "pending";

    public List<OrderItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }

    public decimal Discount { get; set;} = 0;

    public decimal Shiping { get; set; } = 0;

    public decimal Total { get; set; }

    public string Channel { get; set; } = "site";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Customer? Customer { get; set; }
}

public class OrderItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quatity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Size { get; set; }
}