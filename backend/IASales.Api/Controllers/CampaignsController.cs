using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IASales.Api.DTOs;
using IASales.Api.Services;
using System.Data;

namespace IASales.Api.Controllers;

[ApiController]
[Route("api/campaigns")]
[Authorize]
public class CampaignsController : ControllerBase
{
    private readonly ICampaignService _svc;
    public CampaignsController(ICampaignService svc)
    {
        _svc = svc;   
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenandId = GetTenandId();
        var campaigns = await _svc.GetAllAsync(tenandId);
        return Ok(campaigns);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCampaingDTO dto)
    {
        var tenandId =  GetTenandId();
        var campaign = await _svc.CreateAsync(dto, tenandId);
        return Ok(campaign);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusDTO dto)
    {
        var tenandId = GetTenandId();
        var campaign = await _svc.UpdateStatusAsync(id, dto.Status, tenandId);
        return campaign == null ? NotFound() : Ok(campaign);
    }

    private Guid GetTenandId()
    {
        var tenandIdClaim = User.FindFirst("tenand_Id")?.Value;

        if(string.IsNullOrEmpty(tenandIdClaim))
            throw new UnauthorizedAccessException("Tenant não encontrado no token");
        return Guid.Parse(tenandIdClaim);
    }
}