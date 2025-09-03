using CustomerSupportChatBot.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities;

public class SupportAgent : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Department { get; set; }
    public bool IsOnline { get; set; } = false;
    public bool IsAvailable { get; set; } = false;
    public int MaxConcurrentChats { get; set; } = 5;
    public int CurrentActiveChats { get; set; } = 0;
    public DateTime? LastSeenAt { get; set; }

    public virtual ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
}