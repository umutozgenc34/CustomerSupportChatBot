using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Dtos.Requests;

public class SendMessageRequest
{
    public int SessionId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsFromCustomer { get; set; } = true;
    public string? SenderName { get; set; }
}
