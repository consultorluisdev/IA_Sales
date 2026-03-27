namespace IASales.Api.DTOs;

public record ChatMessageDTO(
    string Message,
    Guid? ConversationId,
    string Channel = "web"
);

public record ChatReponseDTO(string Reply, Guid ConversationId);

public record GenerateMarketingDTO(Guid ProductId, string Platform, string Tone);

public record MarketingContentDTO(
    string Post,
    string AdCopy,
    List<string> Hashtags,
    string ImageSuggestion
);
