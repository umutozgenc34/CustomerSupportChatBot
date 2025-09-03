using CustomerSupportChatBot.Core.Entities.Base;
using CustomerSupportChatBot.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities;

public class ChatMessage : BaseEntity
{
    public int ChatSessionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public MessageType MessageType { get; set; }
    public IntentType? DetectedIntent { get; set; }
    public double? IntentConfidence { get; set; } 
    public string? SenderName { get; set; }
    public bool IsFromCustomer { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public bool IsAutoResponse { get; set; } = false;
    public int? AutoResponseId { get; set; } 
    public string? ProcessedKeywords { get; set; } 
    public string? ExtractedEntities { get; set; } 

    public virtual ChatSession ChatSession { get; set; } = null!;
    public virtual AutoResponse? AutoResponse { get; set; }
}
