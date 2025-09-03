using CustomerSupportChatBot.Core.Entities.Base;
using CustomerSupportChatBot.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities;

public class ChatSession : BaseEntity
{
    public string SessionToken { get; set; } = Guid.NewGuid().ToString();
    public int CustomerId { get; set; }
    public int? SupportAgentId { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Active;
    public Priority Priority { get; set; } = Priority.Normal;
    public string? Subject { get; set; }
    public DateTime? StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }
    public DateTime? TransferredToAgentAt { get; set; }
    public TimeSpan? ResponseTime { get; set; } 
    public TimeSpan? ResolutionTime { get; set; } 
    public int MessageCount { get; set; } = 0;
    public bool IsTransferredToAgent { get; set; } = false;
    public string? ClosingNote { get; set; }
    public int? CustomerSatisfactionRating { get; set; } 

    public virtual Customer Customer { get; set; } = null!;
    public virtual SupportAgent? SupportAgent { get; set; }

    public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}