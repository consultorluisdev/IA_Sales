using System.Net.Http.Json;
using System.Text.Json;
using IASales.Api.Data;
using IASales.Api.DTOs;
using IASales.Api.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace IASales.Api.Services;

public class AIAgentService
{
    private readonly HttpClient _http;
    private readonly AppDbContext _ctx;
    private readonly IConfiguration _config;

    public AIAgentService(HttpClient http, AppDbContext ctx, IConfiguration config)
    {
        _http = http;
        _ctx = ctx;
        _config = config;
    }

    public async Task<ChatReponseDTO> ChatAsync(ChatMessageDTO dto, Guid tenantId)
    {
        IAConversation conversation;

        if (dto.ConversationId.HasValue)
        {
            conversation = await _ctx.AIConversations
            .FirstOrDefaultAsync(c => c.Id == dto.ConversationId.Value)
            ?? throw new Exception("Conversa não encontrada.");
        }
        else
        {
            var products = await _ctx.Products
                .Take(20)
                .Select(p => $"{p.Name} R${p.Price:F2}")
                .ToListAsync();
            
            var systemPrompt = $"""
                Você é assistente de vendas. Produtos disponiveis:
                {string.Join("\n", products)}
                Seja amigável, sugira produtos e ajude a fechar vendas.
                Reponda sempre em português.
                """;

            conversation = new IAConversation
            {
                TenantId = tenantId,
                Channel = dto.Channel,
                Messages = new List<ChatMessage>
                {
                    new() { Role = "system", Content = systemPrompt }
                }
            };
            _ctx.AIConversations.Add(conversation);
        }

        conversation.Messages.Add(new ChatMessage
        {
            Role = "user",
            Content = dto.Message
        });

        var reply = await CallClaudeAsync(conversation.Messages);

        conversation.Messages.Add(new ChatMessage
        {
            Role = "assistant",
            Content = reply
        });

        await _ctx.SaveChangesAsync();

        return new ChatReponseDTO(reply, conversation.Id);
    }

    public async Task<MarketingContentDTO> GenerateMarketingAsync(
        GenerateMarketingDTO dto, Guid tenandId)
    {
        var product = await _ctx.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId)
        ?? throw new Exception("Produto não encontrado.");

        var jsonFormat = "{\"post\": \"texto do post\", \"adCopy\": \"texto do anúncio\", \"hashtags\": [\"tag1\", \"tag2\"], \"imageSuggestion\": \"descrição da imagem\"}";
        var prompt = $"""
            Crie conteúdo de marketing para:
            Produto: {product.Name}
            Preço: R${product.Price:F2}
            Plataforma: {dto.Platform}
            Tom: {dto.Tone}

            Retorne APENAS JSON válido no formato:
            {jsonFormat}
            """;

        var json = await CallClaudeAsync(new List<ChatMessage>
        {
            new() { Role = "user", Content = prompt }
        });
        
        return JsonSerializer.Deserialize<MarketingContentDTO>(json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        ?? throw new Exception("Falha ao processar resposta da IA.");
    }

    private async Task<string> CallClaudeAsync(List<ChatMessage> messages)
    {
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("x-api-key", _config["Antrophic:ApiKey"]);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        var system = messages.FirstOrDefault(m => m.Role == "system")?.Content ?? "";
        var msgs = messages
            .Where(m => m.Role != "system")
            .Select(m => new { role = m.Role, content = m.Content });

        var body = new { model = "claude-sonnet-4-20225014", max_tokens = 1024, system, messages = msgs };
        var response = await _http.PostAsJsonAsync("https://api.anthropic.com/v1/messages", body);
        var data = await response.Content.ReadFromJsonAsync<JsonElement>();
        return data.GetProperty("content")[0].GetProperty("text").GetString() ?? "";
    }
}