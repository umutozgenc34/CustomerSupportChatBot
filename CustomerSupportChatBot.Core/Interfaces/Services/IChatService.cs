using CustomerSupportChatBot.Core.Dtos.Requests;
using CustomerSupportChatBot.Core.Dtos.Responses;
using CustomerSupportChatBot.Core.Dtos.Responses.ReturnModel;

namespace CustomerSupportChatBot.Core.Interfaces.Services;

public interface IChatService
{
    Task<ChatSessionResponse> StartSessionAsync(CreateSessionRequest request);
    Task<ChatMessageResponse> SendMessageAsync(SendMessageRequest request);
    Task<ChatSessionResponse> GetSessionAsync(int sessionId);
    Task<IEnumerable<ChatMessageResponse>> GetSessionMessagesAsync(int sessionId);
    Task<ChatSessionResponse> CloseSessionAsync(int sessionId, string? closingNote = null);
    Task<ChatSessionResponse> TransferToAgentAsync(TransferToAgentRequest request);
    Task<bool> IsSessionActiveAsync(int sessionId);
    Task<ApiResponse<string>> GetSessionStatusAsync(string sessionToken);
    Task MarkMessagesAsReadAsync(int sessionId);
}