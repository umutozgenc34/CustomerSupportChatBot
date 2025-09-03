using CustomerSupportChatBot.Core.Entities.Base;
using CustomerSupportChatBot.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities;

public class AutoResponse : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IntentType IntentType { get; set; }
    public string Keywords { get; set; } = string.Empty; 
    public string? Patterns { get; set; } 
    public Priority Priority { get; set; } = Priority.Normal;
    public bool IsActive { get; set; } = true;
    public int UsageCount { get; set; } = 0;
    public double SuccessRate { get; set; } = 0.0; 
    public int Order { get; set; } = 0; 

    public string? RequiredContext { get; set; } 
    public string? FollowUpQuestions { get; set; } 

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
}