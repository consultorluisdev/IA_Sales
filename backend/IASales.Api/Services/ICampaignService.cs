using IASales.Api.DTOs;
using IASales.Api.Entities;

namespace IASales.Api.Services;

public interface ICampaignService
{
    Task<List<Campaing>> GetAllAsync(Guid tenantId);
    Task<Campaing> CreateAsync(CreateCampaingDTO dto, Guid tenantId);
    Task<Campaing?> UpdateStatusAsync(Guid id, string status, Guid tenantId);
}
