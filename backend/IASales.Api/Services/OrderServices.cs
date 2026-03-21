using Microsoft.EntityFrameworkCore;
using IASales.Api.Data;
using IASales.Api.DTOs;
using IASales.Api.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace IASales.Api.Services;

public class OrderServices
{
    private readonly AppDbContext _ctx

    public OrderServices(AppDbContext ctx) => _ctx = ctx;

    public async Task<List<Order>> GetAllAsync(Guid tenantId)
    {
        return await _ctx.Orders
        .Include(o => o.Customer)
        .Where(o => o.TenantId == tenantId)
        .OrderByDescending(o => o.CreatedAt)
        .ToListAsync();
    }
    public async Task<Order?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _ctx.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id && o.TenantId == tenantId);
    }
    public async Task<Order?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _ctx.Orders
        .Include(o => o.Customer)
        .FirstOrDefaultAsync(o => o.Id == id && o.TenantId == tenantId);
    }
    public async Task<Order> CreateAsync(CreateOrderDTO dto, Guid tenantId)
    {
        var subtotal = dto.Items.Sum(i => i.UnitPrice * i.Quatity);

        var order = new Order
        {
            TenantId = tenantId,
            CustomerId = dto.CustomerId,
            Items = dto.Items,
            Subtotal = subtotal,
            Discount = dto.Discount,
            Shiping = dto.Shipping,
            Total = subtotal - dto.Discount + dto.Shipping,
            Channel = dto.Channel
        };
        _ctx.Orders.Add(order);

        // atualiza o status do cliente
        if(dto.CustomerId.HasValue)
        {
            var customer = await _ctx.Customers.FindAsync(dto.CustomerId.Value);
            if(customer != null)
            {
                customer.TotalSpent += order.Total;
                customer.OrderCount++;
            }
        }
        await _ctx.SaveChangesAsync();
        return order;
    }
    public async Task<Order?> UpdateStatusAsync(Guid id, string status, Guid tenantId)
    {
        var order = await _ctx.Orders
            .FirstOrDefaultAsync(o => o.Id == id && o.TenantId == tenantId);
        
        if(order == null) return null;

        order.Status = status;
        await _ctx.SaveChangesAsync();
        return order;
    }
}