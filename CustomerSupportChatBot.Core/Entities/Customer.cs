using CustomerSupportChatBot.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? SessionId { get; set; } 
    public string? UserAgent { get; set; }
    public string? IpAddress { get; set; }

    public virtual ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
}
