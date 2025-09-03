using CustomerSupportChatBot.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Dtos.Requests;

public class TransferToAgentRequest
{
    public int SessionId { get; set; }
    public string? Reason { get; set; }
    public Priority Priority { get; set; } = Priority.Normal;
    public string? PreferredDepartment { get; set; }
    public int? PreferredAgentId { get; set; }
}
