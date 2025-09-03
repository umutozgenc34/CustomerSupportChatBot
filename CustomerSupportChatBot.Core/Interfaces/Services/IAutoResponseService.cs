using CustomerSupportChatBot.Core.Dtos.Responses;
using CustomerSupportChatBot.Core.Entities;

namespace CustomerSupportChatBot.Core.Interfaces.Services;

public interface IAutoResponseService
{
    Task<ChatMessageResponse?> GetAutoResponseAsync(string message, int sessionId);
    Task<IEnumerable<AutoResponse>> GetSuggestedResponsesAsync(string message);
    Task<AutoResponse> CreateAutoResponseAsync(AutoResponse autoResponse);
    Task<AutoResponse> UpdateAutoResponseAsync(AutoResponse autoResponse);
    Task DeleteAutoResponseAsync(int responseId);
    Task<bool> TestResponseMatchAsync(int responseId, string testMessage);
    Task UpdateResponseStatisticsAsync(int responseId, bool wasHelpful);
}