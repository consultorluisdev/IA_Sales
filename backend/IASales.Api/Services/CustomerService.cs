using Microsoft.EntityFrameworkCore;
using IASales.Api.Data;
using IASales.Api.DTOs;
using IASales.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace IASales.Api.Services;

public class CustomerService
{
    private readonly AppDbContext _ctx;

    public CustomerService(AppDbContext ctx) => _ctx = ctx;

    public async Task<List<Customer>> GetAllAsync(Guid tenantId)
    {
        return await _ctx.Customers
        .Where(c => c.TenantId == tenantId)
        .OrderByDescending(c => c.CreatedAt)
        .ToListAsync();
    }
    public async Task<Customer?> GetByIdAsync(Guid id, Guid tenandId)
    {
        return await _ctx.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenandId);
    }
    public async Task<Customer> CreateAsync(CreateCustomerDTO dto, Guid tenandId)
    {
        var customer = new Customer
        {
            TenantId = tenandId,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone ?? "",
            Source = dto.Source,
            Interests = dto.Interests ?? new List<string>(),
            Notes = dto.Notes ?? ""
        };
        _ctx.Customers.Add(customer);
        await _ctx.SaveChangesAsync();
        return customer;
    }
    public async Task<Customer?> UpdateAsync(Guid id, UpdateCustomerDTO dto, Guid tenantId)
    {
        var customer = await _ctx.Customers
            .FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);

        if(customer == null) return null;

        if(dto.Name != null) customer.Name = dto.Name;
        if(dto.Email != null) customer.Email = dto.Email;
        if(dto.Phone != null) customer.Phone = dto.Phone;
        if(dto.Notes != null) customer.Notes = dto.Notes;

        await _ctx.SaveChangesAsync();
        return customer;
    }
    public async Task<bool> DeleteAsync(Guid id, Guid tenantId)
    {
        var customer = await _ctx.Customers
            .FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
        if(customer == null) return false;

        _ctx.Customers.Remove(customer);
        await _ctx.SaveChangesAsync();
        return true;
    }
}