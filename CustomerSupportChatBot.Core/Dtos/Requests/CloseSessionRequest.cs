using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Dtos.Requests;

public class CloseSessionRequest
{
    public int SessionId { get; set; }
    public string? ClosingNote { get; set; }
    public int? CustomerSatisfactionRating { get; set; }
}