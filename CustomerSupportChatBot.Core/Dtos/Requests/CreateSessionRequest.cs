using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Dtos.Requests;

public class CreateSessionRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string? CustomerPhone { get; set; }
    public string? Subject { get; set; }
    public string InitialMessage { get; set; } = string.Empty;
    public string? SessionId { get; set; } 
    public string? UserAgent { get; set; }
    public string? IpAddress { get; set; }
}
