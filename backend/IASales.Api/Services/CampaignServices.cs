using Microsoft.EntityFrameworkCore;
using IASales.Api.Data;
using IASales.Api.DTOs;
using IASales.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IASales.Api.Services;

public class CampaignServices
{
    private readonly AppDbContext _ctx;
    private readonly AIAgentService _ai;

    public CampaignServices(AppDbContext ctx, AIAgentService ai)
    {
        _ctx = ctx;
        _ai = ai;
    }
    public async Task<List<Campaing>> GetAllAsync(Guid tenandId)
    {
        return await _ctx.Campaigns
            .Where(c => c.TenantId == tenandId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
    public async Task<Campaing> CreateAsync(CreatedCampingDTO dto, Guid tenandId)
    {
        var campign = new Campaing
        {
            TenantId = tenandId,
            Name = dto.Name,
            Platform = dto.Platform,
            Objective = dto.Objective,
            BudgetDaily = dto.BudgetDaly,
            AIGenerated = dto.GenerateWithAI
        };


        // gera conteudo com IA se solicitado
        if (dto.GenerateWithAI && dto.ProductId.HasValue)
        {
            var product = await _ctx.Products.FindAsync(dto.ProductId.Value);
            if (product != null)
            {
                    var content = await _ai.GenerateMarketingAsync(
                        new GenerateMarketingDTO(dto.ProductId.Value, dto.Platform, "vendas"),
                        tenandId);

                    campign.Adcopy = content.AdCopy;
                    campign.Headline = content.Post;
                }
            }
            _ctx.Campaigns.Add(campign);
            await _ctx.SaveChangesAsync();
            return campign;
        }
    }    

    public async Task<Campaing?> UpdateStatusAsync(Guid id, string status, Guid tenandId)
    {
        var campaing = await _ctx.Campaigns
        .FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenandId);

        if (campaing == null) return null;

        campaing.Status = status;
        await _ctx.SaveChangesAsync();
        return campaing;
    }

}
