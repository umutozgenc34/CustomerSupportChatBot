using CustomerSupportChatBot.Core.Enums;

namespace CustomerSupportChatBot.Core.Dtos.Responses;

public class NLPAnalysisResponse
{
    public string OriginalMessage { get; set; } = string.Empty;
    public IntentType DetectedIntent { get; set; }
    public double Confidence { get; set; }
    public IEnumerable<string> Keywords { get; set; } = new List<string>();
    public Dictionary<string, object> ExtractedEntities { get; set; } = new();
    public bool IsGreeting { get; set; }
    public bool IsGoodbye { get; set; }
    public bool RequiresHumanSupport { get; set; }
    public string? SuggestedResponse { get; set; }
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
}