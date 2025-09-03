using CustomerSupportChatBot.Core.Enums;

namespace CustomerSupportChatBot.Core.Dtos.Responses;

public class ChatSessionResponse
{
    public int Id { get; set; }
    public string SessionToken { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public SessionStatus Status { get; set; }
    public Priority Priority { get; set; }
    public string? Subject { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public bool IsTransferredToAgent { get; set; }
    public string? SupportAgentName { get; set; }
    public int MessageCount { get; set; }
    public int UnreadMessageCount { get; set; }
    public TimeSpan? ResponseTime { get; set; }
    public TimeSpan? ResolutionTime { get; set; }
}
