using IASales.Api.Entities;

namespace IASales.Api.DTOs;

public record CreateOrderDTO(
    Guid? CustomerId,
    List<OrderItem> Items,
    string Channel,
    decimal Discount = 0,
    decimal Shipping = 0
);

public record UpdateStatusDTO(string Status);