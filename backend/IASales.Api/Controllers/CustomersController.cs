using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IASales.Api.DTOs;
using IASales.Api.Services;

namespace IASales.Api.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _svc;

    public CustomersController(CustomerService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenantId = GetTenantId();
        var customers = await _svc.GetAllAsync(tenantId);
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenantId = GetTenantId();
        var customer = await _svc.GetByIdAsync(id, tenantId);
        return customer == null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDTO dto)
    {
        var tenantId = GetTenantId();
        var customer = await _svc.CreateAsync(dto, tenantId);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDTO dto)
    {
        var tenantId = GetTenantId();
        var customer = await _svc.UpdateAsync(id, dto, tenantId);
        return customer == null ? NotFound() : Ok(customer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tenantId = GetTenantId();
        var result = await _svc.DeleteAsync(id, tenantId);
        return result ? NoContent() : NotFound();
    }
    private Guid GetTenantId() =>
    Guid.Parse(User.FindFirst("tenant_id")?.Value ?? Guid.Empty.ToString());
}
