using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IASales.Api.DTOs;
using IASales.Api.Services;

namespace IASales.Api.Controllers;
[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly OrderServices _svc;

    public OrderController(OrderServices svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenantId = GetTenantId();
        var orders = await _svc.GetAllAsync(tenantId);
        return Ok(orders);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusDTO dto)
    {
        var tenantId = GetTenantId();
        var order = await _svc.UpdateStatusAsync(id, dto.Status, tenantId);
        return order == null ? NotFound() : Ok(order);
    }
    private Guid GetTenantId() => 
        Guid.Parse(User.FindFirst("tenant_id")?.Value ?? Guid.Empty.ToString());
}