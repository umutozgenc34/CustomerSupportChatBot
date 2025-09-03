using CustomerSupportChatBot.Core.Enums;

namespace CustomerSupportChatBot.Core.Dtos.Responses;

public class ChatMessageResponse
{
    public int Id { get; set; }
    public int ChatSessionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public MessageType MessageType { get; set; }
    public IntentType? DetectedIntent { get; set; }
    public double? IntentConfidence { get; set; }
    public string? SenderName { get; set; }
    public bool IsFromCustomer { get; set; }
    public bool IsRead { get; set; }
    public bool IsAutoResponse { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}
