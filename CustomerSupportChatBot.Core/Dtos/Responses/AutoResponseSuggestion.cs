namespace CustomerSupportChatBot.Core.Dtos.Responses;

public class AutoResponseSuggestion
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public double MatchScore { get; set; }
    public string MatchReason { get; set; } = string.Empty;
}